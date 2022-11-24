Imports System.Net
Imports System.Net.Sockets

Public Class frmPrincipale

    'horloge
    Private restantBlanc As Integer
    Private restantNoir As Integer
    Private increment As Integer
    Private pause As Boolean
    Private gameStart As Long
    Private endGame As Long

    'pgn
    Private nbMoves As Integer
    Private positionEngine As String
    Private positionFR As String
    Private moveEngine As String
    Private tabFEN() As String
    Private iFEN As Integer
    Const sitePGN = "LAN 10GBE"
    Private roundPGN As Integer
    Private chaineEPD As String

    'fin de partie
    Private repetition As Boolean
    Private maxNul As Integer
    Private maxScore As Integer
    Const seuilScore = 5
    Const maxMoves = 200
    Private fiftyMoves As Integer
    Private nbDrawScore As Integer
    Private nbWhiteScores As Integer
    Private nbBlackScores As Integer

    'réseau
    Private waiting As Boolean
    Private clientReady As Boolean
    Private whitePlayer As String
    Private blackPlayer As String

    'statistiques
    Private nbGames As Integer
    Private whiteScore As Single
    Private blackScore As Single
    Private serverScore As Single
    Private clientScore As Single
    Private drawGames As Single
    Private totEcoule As Integer
    Private nbEcoule As Integer
    Private totDepthServer As Integer
    Private totDepthClient As Integer
    Private maxDepthServer As Integer
    Private maxDepthClient As Integer
    Private totSpeedServer As Integer
    Private totSpeedClient As Integer
    Private maxVitServeur As Integer
    Private maxVitClient As Integer
    Private totPredictServer As Integer
    Private totPredictClient As Integer
    Private totDuration As Integer

    Private dpi As Single

    Private Sub frmPrincipale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pause = False
        modePonder = "0"
        roundPGN = 1
        chaineEPD = ""
        iFEN = -1
        ReDim tabFEN(0)
        tabFEN(0) = ""
        whitePlayer = ""
        blackPlayer = ""
        nbGames = 1
        whiteScore = 0
        blackScore = 0
        serverScore = 0
        clientScore = 0
        drawGames = 0
        totEcoule = 0
        nbEcoule = 0
        totDepthServer = 0
        totDepthClient = 0
        maxDepthServer = 0
        maxDepthClient = 0
        totSpeedServer = 0
        totSpeedClient = 0
        maxVitServeur = 0
        maxVitClient = 0
        totPredictServer = 0
        totPredictClient = 0
        totDuration = 0
        crash = False
        pbEchiquier.BackgroundImage = Image.FromFile("BMP\echiquier.bmp")
        pbMateriel.BackColor = Color.FromName("control")

        dpi = Me.CreateGraphics.DpiX

        initialisation()

        chargerParametres()
    End Sub

    Private Sub frmPrincipale_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        analyseEnCours = True
        timerCommunication.Enabled = False
        arretMoteur()

        Try
            If rbServer.Checked Then
                transmission("end", socketMessage)
            ElseIf rbClient.Checked Then
                transmission("end", socketConnexion)
            End If
        Catch ex As Exception

        End Try

        End
    End Sub

    Private Sub rbServeur_CheckedChanged(sender As Object, e As EventArgs) Handles rbServer.CheckedChanged
        If rbServer.Checked Then
            rbServer.Text = "Server"
            rbClient.Text = "Client"
            cmdConnexion.Text = "LISTEN"
            cmdConnexion.Enabled = True
            cbPonder.Enabled = True
            txtDelay.Enabled = True
            txtIncrement.Enabled = True
            cmdFEN.Enabled = True
            txtFEN.ReadOnly = False
        End If
    End Sub

    Private Sub rbClient_CheckedChanged(sender As Object, e As EventArgs) Handles rbClient.CheckedChanged
        If rbClient.Checked Then
            rbClient.Text = "Client"
            rbServer.Text = "Server"
            cmdConnexion.Text = "CONNECT"
            cmdConnexion.Enabled = True
            cbPonder.Enabled = False
            txtDelay.Enabled = False
            txtIncrement.Enabled = False
            cmdFEN.Enabled = False
            txtFEN.ReadOnly = True
        End If
    End Sub

    Private Sub cbPonder_CheckedChanged(sender As Object, e As EventArgs) Handles cbPonder.CheckedChanged
        If cbPonder.Checked Then
            modePonder = "1"
        Else
            modePonder = "0"
        End If
    End Sub

    Private Sub cmdFEN_Click(sender As Object, e As EventArgs) Handles cmdFEN.Click
        ofdLAN.FileName = ""
        ofdLAN.Filter = "Position EPD (*.epd)|*.epd"
        If ofdLAN.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtFEN.Text = ofdLAN.FileName
        End If
    End Sub

    Private Sub cmdEngine_Click(sender As Object, e As EventArgs) Handles cmdEngine.Click
        ofdLAN.FileName = ""
        ofdLAN.Filter = "Engine EXE (*.exe)|*.exe"
        If ofdLAN.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtEngine.Text = ofdLAN.FileName
        End If
    End Sub

    Private Sub cmdConnexion_Click(sender As Object, e As EventArgs) Handles cmdConnexion.Click
        Dim ip As IPEndPoint

        If cmdConnexion.Text = "PAUSE" Then
            cmdConnexion.Text = "RESUME"
            pause = True
            Exit Sub
        ElseIf cmdConnexion.Text = "RESUME" Then
            cmdConnexion.Text = "PAUSE"
            pause = False
            Exit Sub
        ElseIf txtEngine.Text = "" Then
            pause = False
            Exit Sub
        End If

        cmdConnexion.Enabled = False
        rbClient.Enabled = False
        rbServer.Enabled = False
        cbPonder.Enabled = False
        txtDelay.Enabled = False
        txtIncrement.Enabled = False
        txtThreads.Enabled = False
        txtHash.Enabled = False
        cmdFEN.Enabled = False
        cmdEngine.Enabled = False
        txtFEN.ReadOnly = True
        txtEngine.ReadOnly = True
        If My.Computer.FileSystem.FileExists(txtFEN.Text) Then
            tabFEN = Split(My.Computer.FileSystem.ReadAllText(txtFEN.Text), vbCrLf)
            While tabFEN(UBound(tabFEN)) = ""
                ReDim Preserve tabFEN(UBound(tabFEN) - 1)
                Application.DoEvents()
            End While
            txtFEN.Text = tabFEN(0)
            lblFEN.Text = "FEN 1/" & tabFEN.Length & " :"
        Else
            txtFEN.Text = ""
            lblFEN.Text = "FEN :"
        End If

        restantBlanc = Val(txtDelay.Text) * 1000
        restantNoir = restantBlanc
        horlogeBlanc(restantBlanc)
        horlogeNoir(restantNoir)
        memRestant = restantBlanc
        increment = Val(txtIncrement.Text)
        verification()

        'on sauvegarde les paramètres
        sauverParametres()

        socketConnexion = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

        If rbServer.Checked Then
            modeReseau = "server"
            ip = New IPEndPoint(IPAddress.Parse(adresseIPv4(My.Computer.Name)), 50000)

            rbServer.Text = "Server (" & ip.Address.ToString & ")"
            lstMessage.Items.Add(Format(Now, "HH:mm:ss") & " waiting for connection...")
            lstMessage.SelectedIndex = lstMessage.Items.Count - 1
            Application.DoEvents()

            Try
                socketConnexion.Bind(ip)
                socketConnexion.Listen(1)
                socketMessage = socketConnexion.Accept()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "server")
                End
            End Try

            ip = socketMessage.RemoteEndPoint
            rbClient.Text = "Client (" & ip.Address.ToString & ")"

            lstMessage.Items.Add(Format(Now, "HH:mm:ss") & " connection established.")
            lstMessage.Items.Add("")
            lstMessage.SelectedIndex = lstMessage.Items.Count - 1
            Application.DoEvents()

            chargerStats()

        ElseIf rbClient.Checked Then
            modeReseau = "client"
            lstMessage.Items.Add(Format(Now, "HH:mm:ss") & " connecting...")
            lstMessage.SelectedIndex = lstMessage.Items.Count - 1
            Application.DoEvents()

            Try
                socketConnexion.Connect(InputBox("Enter the server's name or ip address :", , My.Computer.Name), 50000)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "client")
                End
            End Try

            ip = socketConnexion.RemoteEndPoint
            rbServer.Text = "Server (" & ip.Address.ToString & ")"
            ip = socketConnexion.LocalEndPoint
            rbClient.Text = "Client (" & ip.Address.ToString & ")"

            lstMessage.Items.Add(Format(Now, "HH:mm:ss") & " connection established.")
            lstMessage.Items.Add("")
            lstMessage.SelectedIndex = lstMessage.Items.Count - 1
            Application.DoEvents()
        End If

        'client et serveur connectés, moteurs chargés, variables initialisées
        cmdConnexion.Text = "PAUSE"
        cmdConnexion.Enabled = True

        timerCommunication.Enabled = True
    End Sub

    Private Sub timerCommunication_Tick(sender As Object, e As EventArgs) Handles timerCommunication.Tick
        Dim message As String, tabMessage() As String

        If analyseEnCours Or (modePonder = "0" And pause) Then
            Exit Sub
        End If

        timerCommunication.Enabled = False

        message = ""
        If rbServer.Checked Then
            'on attend que le serveur décide qui commence
            If nbMoves = -1 Then
                If modePonder = "1" And pause Then
                    If Me.Text <> "pause" Then
                        Me.Text = "pause"
                    End If
                    timerCommunication.Enabled = True
                    Exit Sub
                End If
                nbMoves = 0
                'on alterne
                If Not crash Then
                    If whitePlayer <> My.Computer.Name & " (server)" Then
                        whitePlayer = My.Computer.Name & " (server)"
                        blackPlayer = "CLIENT"

                        If txtFEN.Text <> "" Then
                            iFEN = iFEN + 1
                            If iFEN > UBound(tabFEN) Then
                                iFEN = 0
                                roundPGN = roundPGN + 1
                            End If
                            txtFEN.Text = tabFEN(iFEN)
                            lblFEN.Text = "FEN " & Format(iFEN + 1) & "/" & tabFEN.Length & " :"
                        Else
                            iFEN = 0
                        End If
                    ElseIf whitePlayer = My.Computer.Name & " (server)" Then
                        whitePlayer = "CLIENT"
                        blackPlayer = My.Computer.Name & " (server)"
                    End If
                Else
                    crash = False
                    chargerStats(True)
                End If
                chargerMoteur(txtEngine.Text, Val(txtThreads.Text), Val(txtHash.Text))
            End If

            If whitePlayer = My.Computer.Name & " (server)" And Not clientReady And Not waiting And nbMoves = 0 Then
                message = "black:" & nbMoves & ":" & restantBlanc & ":" & restantNoir & ":" & increment & ":" & tabFEN(iFEN) & ":" & whitePlayer & ":" & nbGames & ":" & roundPGN & ":" _
                        & Format(whiteScore, "0.0") & ":" & Format(blackScore, "0.0") & ":" & Format(serverScore, "0.0") & ":" & Format(clientScore, "0.0") & ":" & Format(drawGames, "0.0") & ":" _
                        & nbEcoule & ":" & totEcoule & ":" & totDepthServer & ":" & totDepthClient & ":" & maxDepthServer & ":" & maxDepthClient & ":" & totDuration & ":" _
                        & totSpeedServer & ":" & totSpeedClient & ":" & maxVitServeur & ":" & maxVitClient & ":" & totPredictServer & ":" & totPredictClient & ":" & modePonder
                'on prévient le client qu'il a les noirs
                transmission(message, socketMessage)
                message = ""
                'on attend que le client soit prêt
                waiting = True

            ElseIf blackPlayer = My.Computer.Name & " (server)" And Not clientReady And Not waiting And nbMoves = 0 Then
                message = "white:" & nbMoves & ":" & restantBlanc & ":" & restantNoir & ":" & increment & ":" & tabFEN(iFEN) & ":" & blackPlayer & ":" & nbGames & ":" & roundPGN & ":" _
                        & Format(whiteScore, "0.0") & ":" & Format(blackScore, "0.0") & ":" & Format(serverScore, "0.0") & ":" & Format(clientScore, "0.0") & ":" & Format(drawGames, "0.0") & ":" _
                        & nbEcoule & ":" & totEcoule & ":" & totDepthServer & ":" & totDepthClient & ":" & maxDepthServer & ":" & maxDepthClient & ":" & totDuration & ":" _
                        & totSpeedServer & ":" & totSpeedClient & ":" & maxVitServeur & ":" & maxVitClient & ":" & totPredictServer & ":" & totPredictClient & ":" & modePonder
                'on prévient le client qu'il a les blancs (et on attend que le client envoie son nom)
                transmission(message, socketMessage)
                message = ""
                'on attend que le client soit prêt
                waiting = True

            Else 'on attend le coup du client
                message = reception(socketMessage)

                If message = "crash" Then
                    message = ""
                    crash = True
                    arretMoteur()
                    initialisation()
                ElseIf message = "end" Then
                    End
                ElseIf InStr(message, "stop:", CompareMethod.Text) = 1 Then 'le client a demandé l'arrêt et transmet la dernière position
                    nbGames = nbGames + 1
                    tabMessage = Split(message, ":")
                    message = ""

                    positionFR = tabMessage(1)
                    whiteScore = CSng(tabMessage(2))
                    blackScore = CSng(tabMessage(3))
                    serverScore = CSng(tabMessage(4))
                    clientScore = CSng(tabMessage(5))
                    drawGames = CSng(tabMessage(6))
                    endGame = Val(tabMessage(7))
                    totDuration = totDuration + (endGame - gameStart) / 1000

                    sauverStats()
                    My.Computer.FileSystem.WriteAllText("check.pgn", structurePGN(coupsEN(positionFR), tabFEN(iFEN), whitePlayer, blackPlayer, sitePGN, roundPGN), True)
                    arretMoteur()
                    initialisation()
                ElseIf InStr(message, "white_player:", CompareMethod.Text) = 1 Then
                    tabMessage = Split(message, ":")
                    message = ""
                    'on récupère le nom du client et la date de départ de la partie
                    whitePlayer = tabMessage(1)
                    gameStart = Val(tabMessage(3))
                    'on prévient le client que le serveur est prêt
                    transmission("server_ready", socketMessage)
                ElseIf InStr(message, "black_player:", CompareMethod.Text) = 1 Then 'le client est prêt ?
                    blackPlayer = message.Substring(message.IndexOf(":") + 1)
                    message = ""
                    'on envoie la date de début de la partie
                    gameStart = Now.Ticks / 10000
                    transmission("start_game:" & gameStart, socketMessage)
                ElseIf message = "client_ready" Then 'le client a bien reçu la date de départ de la partie
                    clientReady = True
                    waiting = False
                    'le serveur commence avec les blancs
                    message = nbMoves & ":" & "" & ":" & "" & ":" & "" & ":" & "" & ":" & "" & ":" & restantBlanc & ":" & restantNoir & ":" & increment & ":" & tabFEN(iFEN) & ":" & ""

                    'si position epd = a2-a3 donc aux noirs de commencer
                    If InStr(tabFEN(iFEN), " b ") > 0 Then
                        'on simule un coup pour rien des blancs
                        nbMoves = 1
                        nbEcoule = nbEcoule + 1
                        eval = "0,00"
                        score = score & eval & " "
                        positionFR = "1. ... "

                        majAffichage()

                        message = nbMoves & ":" & "..." & ":" & eval & ":" & "0" & ":" & "0" & ":" & "0" & ":" & restantBlanc & ":" & restantNoir & ":" & increment & ":" & tabFEN(iFEN) & ":" & ""
                        lstMessage.Items.Add(Format(Now, "HH:mm:ss") & " black to start")
                        lstMessage.Items.Add(" ")
                        lstMessage.SelectedIndex = lstMessage.Items.Count - 1
                        Application.DoEvents()

                        transmission(message, socketMessage)
                        message = ""
                        trait = "black"
                    End If
                End If

                If message <> "" Then
                    traitement(message, socketMessage)
                    message = ""
                    If Not crash Then
                        If nbMoves > 0 Then
                            waiting = False
                        End If
                        majAffichage()
                    Else
                        transmission("crash", socketMessage)
                        arretMoteur()
                        initialisation()
                    End If
                End If
            End If

        ElseIf rbClient.Checked Then

            'tant qu'on ne sait pas qui commence, on consulte la réception
            If whitePlayer = "" Then
                If modePonder = "1" And pause Then
                    If Me.Text <> "pause" Then
                        Me.Text = "pause"
                    End If
                    timerCommunication.Enabled = True
                    Exit Sub
                End If
                message = reception(socketConnexion)
                If message = "" Then
                    timerCommunication.Enabled = True
                    Exit Sub
                End If
            End If

            'qui commence ?
            If InStr(message, "black:", CompareMethod.Text) = 1 Then
                'le serveur a les blancs
                tabMessage = Split(message, ":")
                message = ""

                nbMoves = Val(tabMessage(1))

                restantBlanc = Val(tabMessage(2))
                memRestant = restantBlanc
                txtDelay.Text = restantBlanc / 1000

                restantNoir = Val(tabMessage(3))
                horlogeBlanc(restantBlanc)
                horlogeNoir(restantNoir)

                increment = Val(tabMessage(4))
                txtIncrement.Text = increment

                chargerMoteur(txtEngine.Text, Val(txtThreads.Text), Val(txtHash.Text))

                txtFEN.Text = tabMessage(5) 'attention : il s'agit de la chaine FEN et pas du chemin du fichier EPD

                whitePlayer = tabMessage(6)
                blackPlayer = My.Computer.Name & " (client)"

                nbGames = Val(tabMessage(7))
                roundPGN = Val(tabMessage(8))

                whiteScore = CSng(tabMessage(9))
                blackScore = CSng(tabMessage(10))

                serverScore = CSng(tabMessage(11))
                clientScore = CSng(tabMessage(12))
                drawGames = CSng(tabMessage(13))

                nbEcoule = Val(tabMessage(14))
                totEcoule = Val(tabMessage(15))

                totDepthServer = Val(tabMessage(16))
                totDepthClient = Val(tabMessage(17))
                maxDepthServer = Val(tabMessage(18))
                maxDepthClient = Val(tabMessage(19))

                totDuration = Val(tabMessage(20))

                totSpeedServer = Val(tabMessage(21))
                totSpeedClient = Val(tabMessage(22))
                maxVitServeur = Val(tabMessage(23))
                maxVitClient = Val(tabMessage(24))

                totPredictServer = Val(tabMessage(25))
                totPredictClient = Val(tabMessage(26))

                If tabMessage(27) = "0" Then
                    cbPonder.CheckState = CheckState.Unchecked
                ElseIf tabMessage(27) = "1" Then
                    cbPonder.CheckState = CheckState.Checked
                End If

                majAffichage()

                'on prévient le serveur que le client est prêt
                transmission("black_player:" & blackPlayer, socketConnexion)

            ElseIf InStr(message, "white:", CompareMethod.Text) = 1 Then
                'le client a les blancs
                tabMessage = Split(message, ":")
                message = ""

                nbMoves = Val(tabMessage(1))

                restantBlanc = Val(tabMessage(2))
                memRestant = restantBlanc
                txtDelay.Text = restantBlanc / 1000

                restantNoir = Val(tabMessage(3))
                horlogeBlanc(restantBlanc)
                horlogeNoir(restantNoir)

                increment = Val(tabMessage(4))
                txtIncrement.Text = increment

                chargerMoteur(txtEngine.Text, Val(txtThreads.Text), Val(txtHash.Text))

                txtFEN.Text = tabMessage(5) 'attention : il s'agit de la chaine FEN et pas du chemin du fichier EPD

                whitePlayer = My.Computer.Name & " (client)"
                blackPlayer = tabMessage(6)

                nbGames = Val(tabMessage(7))
                roundPGN = Val(tabMessage(8))

                whiteScore = CSng(tabMessage(9))
                blackScore = CSng(tabMessage(10))

                serverScore = CSng(tabMessage(11))
                clientScore = CSng(tabMessage(12))
                drawGames = CSng(tabMessage(13))

                nbEcoule = Val(tabMessage(14))
                totEcoule = Val(tabMessage(15))

                totDepthServer = Val(tabMessage(16))
                totDepthClient = Val(tabMessage(17))
                maxDepthServer = Val(tabMessage(18))
                maxDepthClient = Val(tabMessage(19))

                totDuration = Val(tabMessage(20))

                totSpeedServer = Val(tabMessage(21))
                totSpeedClient = Val(tabMessage(22))
                maxVitServeur = Val(tabMessage(23))
                maxVitClient = Val(tabMessage(24))

                totPredictServer = Val(tabMessage(25))
                totPredictClient = Val(tabMessage(26))

                If tabMessage(27) = "0" Then
                    cbPonder.CheckState = CheckState.Unchecked
                ElseIf tabMessage(27) = "1" Then
                    cbPonder.CheckState = CheckState.Checked
                End If

                majAffichage()

                'on envoie le nom du client au serveur et la date de début de la partie
                gameStart = Now.Ticks / 10000
                transmission("white_player" & ":" & whitePlayer & ":" & "start_game" & ":" & gameStart, socketConnexion)
                waiting = True

            Else 'on attend le coup du serveur
                message = reception(socketConnexion)

                If message = "crash" Then
                    message = ""
                    arretMoteur()
                    initialisation()
                ElseIf message = "end" Then
                    End
                ElseIf InStr(message, "stop:", CompareMethod.Text) = 1 Then 'le serveur demande l'arrêt de la partie
                    tabMessage = Split(message, ":")
                    message = ""

                    positionFR = tabMessage(1)
                    whiteScore = CSng(tabMessage(2))
                    blackScore = CSng(tabMessage(3))
                    serverScore = CSng(tabMessage(4))
                    clientScore = CSng(tabMessage(5))
                    drawGames = CSng(tabMessage(6))
                    endGame = Val(tabMessage(7))
                    totDuration = totDuration + (endGame - gameStart) / 1000

                    arretMoteur()
                    whitePlayer = ""
                    blackPlayer = ""
                    nbGames = nbGames + 1
                    initialisation()
                ElseIf InStr(message, "start_game:", CompareMethod.Text) = 1 Then 'le serveur est prêt ?
                    gameStart = Val(message.Substring(message.IndexOf(":") + 1))
                    message = ""
                    transmission("client_ready", socketConnexion)
                ElseIf InStr(message, "server_ready", CompareMethod.Text) = 1 Then 'le serveur est prêt ?
                    'le client commence avec les blancs
                    message = nbMoves & ":" & "" & ":" & "" & ":" & "" & ":" & "" & ":" & "" & ":" & restantBlanc & ":" & restantNoir & ":" & increment & ":" & txtFEN.Text & ":" & ""

                    'si position epd = a2-a3 donc aux noirs de commencer
                    If InStr(txtFEN.Text, " b ") > 0 Then
                        'on simule un coup pour rien des blancs
                        nbMoves = 1
                        nbEcoule = nbEcoule + 1
                        eval = "0,00"
                        score = score & eval & " "
                        positionFR = "1. ... "

                        majAffichage()

                        message = nbMoves & ":" & "..." & ":" & eval & ":" & "0" & ":" & "0" & ":" & "0" & ":" & restantBlanc & ":" & restantNoir & ":" & increment & ":" & txtFEN.Text & ":" & ""
                        lstMessage.Items.Add(Format(Now, "HH:mm:ss") & " black to start")
                        lstMessage.Items.Add(" ")
                        lstMessage.SelectedIndex = lstMessage.Items.Count - 1
                        Application.DoEvents()

                        transmission(message, socketConnexion)
                        message = ""
                        trait = "black"
                    End If
                End If

                If message <> "" Then
                    traitement(message, socketConnexion)
                    message = ""
                    majAffichage()
                    If crash Then
                        transmission("crash", socketConnexion)
                        arretMoteur()
                        initialisation()
                    End If
                End If
            End If
        End If

        timerCommunication.Enabled = True

    End Sub

    Private Sub chargerParametres()
        Dim taillePolice As Single, chaine As String, tabChaine() As String, i As Integer

        taillePolice = 12
        If dpi = 120 Then
            taillePolice = 8.25
        End If

        If My.Computer.FileSystem.FileExists(My.Computer.Name & ".ini") Then
            chaine = My.Computer.FileSystem.ReadAllText(My.Computer.Name & ".ini")
            tabChaine = Split(chaine, vbCrLf)

            For i = 0 To UBound(tabChaine)
                For Each ctrl In Me.Controls
                    ctrl.Font = New Font(DirectCast(ctrl.Font.fontfamily, System.Drawing.FontFamily), taillePolice)
                    If InStr(tabChaine(i), ctrl.name, CompareMethod.Text) = 1 Then
                        ctrl.text = tabChaine(i).Substring(tabChaine(i).IndexOf(" =") + 3)
                        Exit For
                    End If
                    Application.DoEvents()
                Next

                For Each ctrl In cdrSettings.Controls
                    ctrl.Font = New Font(DirectCast(ctrl.Font.fontfamily, System.Drawing.FontFamily), taillePolice)
                    If InStr(tabChaine(i), ctrl.name, CompareMethod.Text) = 1 Then
                        ctrl.text = tabChaine(i).Substring(tabChaine(i).IndexOf(" =") + 3)
                        Exit For
                    End If
                    Application.DoEvents()
                Next

                For Each ctrl In cdrTourney.Controls
                    If ctrl.name = "lblClock" Or ctrl.name = "lblScores" Or ctrl.name = "lblStats" Or ctrl.name = "lblEndGame" Then
                        ctrl.Font = New Font(DirectCast(ctrl.Font.fontfamily, System.Drawing.FontFamily), taillePolice, FontStyle.Bold)
                    Else
                        ctrl.Font = New Font(DirectCast(ctrl.Font.fontfamily, System.Drawing.FontFamily), taillePolice)
                    End If
                    If InStr(tabChaine(i), ctrl.name, CompareMethod.Text) = 1 Then
                        ctrl.text = tabChaine(i).Substring(tabChaine(i).IndexOf(" =") + 3)
                        Exit For
                    End If
                    Application.DoEvents()
                Next
            Next
        End If

    End Sub

    Private Sub chargerStats(Optional silence As Boolean = False)
        Dim tabChaine() As String, i As Integer, critere As String, valeur As Single, fichier As String, chainePonder As String, chaine As String

        chainePonder = ""
        If modePonder = "1" Then
            chainePonder = "ponder_on"
        ElseIf modePonder = "0" Then
            chainePonder = "ponder_off"
        End If

        fichier = "stats_" & My.Computer.Name & "_" _
                & txtDelay.Text & "sec_" & txtIncrement.Text & "ms_" _
                & txtThreads.Text & "C_" & txtHash.Text & "Mo_" _
                & chainePonder

        If My.Computer.FileSystem.FileExists(fichier & ".ini") Then
            If Not silence Then
                If MsgBox("Load the statistics ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    silence = True
                End If
            End If

            If silence Then
                tabChaine = Split(My.Computer.FileSystem.ReadAllText(fichier & ".ini"), vbCrLf)
                For i = 0 To UBound(tabChaine)
                    If tabChaine(i) <> "" Then
                        critere = gauche(tabChaine(i), tabChaine(i).IndexOf(" = "))
                        chaine = tabChaine(i).Substring(tabChaine(i).IndexOf(" =") + 3)
                        valeur = 0
                        If IsNumeric(chaine) Then
                            valeur = CSng(chaine)
                        End If
                        Select Case critere
                            Case "roundPGN"
                                roundPGN = valeur
                            Case "nbGames"
                                nbGames = valeur + 1
                            Case "iFEN"
                                iFEN = valeur
                                If iFEN > UBound(tabFEN) Then
                                    iFEN = 0
                                End If
                            Case "whitePlayer"
                                whitePlayer = chaine
                            Case "blackPlayer"
                                blackPlayer = chaine
                            Case "whiteScore"
                                whiteScore = valeur
                            Case "blackScore"
                                blackScore = valeur
                            Case "serverScore"
                                serverScore = valeur
                            Case "clientScore"
                                clientScore = valeur
                            Case "drawGames"
                                drawGames = valeur
                            Case "nbEcoule"
                                nbEcoule = valeur
                            Case "totEcoule"
                                totEcoule = valeur
                            Case "totDepthServer"
                                totDepthServer = valeur
                            Case "totDepthClient"
                                totDepthClient = valeur
                            Case "totSpeedServer"
                                totSpeedServer = valeur
                            Case "totSpeedClient"
                                totSpeedClient = valeur
                            Case "totPredictServer"
                                totPredictServer = valeur
                            Case "totPredictClient"
                                totPredictClient = valeur
                            Case "totDuration"
                                totDuration = valeur
                                'Case "modePonder"
                                'modePonder = chaine
                        End Select
                    End If
                    Application.DoEvents()
                Next
            End If
        End If
    End Sub

    Private Sub horlogeBlanc(msec As Integer)
        Dim tabHorloge() As String

        If msec < increment Then
            lblWhiteClock.ForeColor = Color.Red
        Else
            lblWhiteClock.ForeColor = Color.Black
        End If

        If msec < 1000 Then
            lblWhiteClock.Text = "WHITE" & vbCrLf & msec & " ms"
        Else
            tabHorloge = Split(secJHMS(msec / 1000), ";")
            lblWhiteClock.Text = "WHITE" & vbCrLf & Format(Val(tabHorloge(2)), "00") & "M" & Format(Val(tabHorloge(3)), "00")
        End If
    End Sub

    Private Sub horlogeNoir(msec As Integer)
        Dim tabHorloge() As String

        If msec < increment Then
            lblBlackClock.ForeColor = Color.Red
        Else
            lblBlackClock.ForeColor = Color.Black
        End If

        If msec < 1000 Then
            lblBlackClock.Text = "BLACK" & vbCrLf & msec & " ms"
        Else
            tabHorloge = Split(secJHMS(msec / 1000), ";")
            lblBlackClock.Text = "BLACK" & vbCrLf & Format(Val(tabHorloge(2)), "00") & "M" & Format(Val(tabHorloge(3)), "00")
        End If
    End Sub

    Private Sub initialisation()
        nbMoves = -1
        trait = "white"
        positionEngine = ""
        positionPonder = ""
        positionFR = ""
        chaineEPD = ""
        score = ""
        eval = ""
        profondeur = 0
        vitesse = 0
        ecoule = 0
        ponderAdverse = ""
        ponderMoteur = ""

        'fin de partie
        victoireNoir = False
        victoireBlanc = False
        repetition = False
        mate = False
        maxNul = 20
        maxScore = 6
        fiftyMoves = 0
        nbDrawScore = 0
        nbWhiteScores = 0
        nbBlackScores = 0

        'horloge
        restantBlanc = Val(txtDelay.Text) * 1000
        restantNoir = restantBlanc
        increment = Val(txtIncrement.Text)
        lblTraitBlanc.Text = ""
        lblTraitNoir.Text = ""
        gameStart = 0
        endGame = 0

        'réseau
        timerCommunication.Interval = 250
        waiting = False
        clientReady = False

        'affichage
        lstMessage.Items.Clear()
        majEchange()
        majAffichage()

        Application.DoEvents()

        'moteur
        verification()

        'log
        If Not crash Then
            nettoyerLOG()
        End If
    End Sub

    Private Sub majAffichage()
        Me.Text = "Round n°" & roundPGN & ", Game n°" & nbGames & ", Move n°"

        If dpi = 96 And Len(positionFR) > 122 Then
            txtPositionFR.Text = coupsEN(droite(positionFR, 122))
        ElseIf dpi = 120 And Len(positionFR) > 130 Then
            txtPositionFR.Text = coupsEN(droite(positionFR, 130))
        Else
            txtPositionFR.Text = coupsEN(positionFR)
        End If

        If nbGames >= 2 Then
            lblWhiteScore.Text = Format(whiteScore, "0.0") & "/" & Format(nbGames - 1) & " (" & Format(whiteScore / (nbGames - 1), "0%") & ")"
            lblBlackScore.Text = Format(blackScore, "0.0") & "/" & Format(nbGames - 1) & " (" & Format(blackScore / (nbGames - 1), "0%") & ")"
            lblServerScore.Text = Format(serverScore, "0.0") & "/" & Format(nbGames - 1) & " (" & Format(serverScore / (nbGames - 1), "0%") & ")"
            lblClientScore.Text = Format(clientScore, "0.0") & "/" & Format(nbGames - 1) & " (" & Format(clientScore / (nbGames - 1), "0%") & ")"
            lblDrawGames.Text = Format(drawGames / (nbGames - 1), "0%") & " of draw game"

            lblGameMoves.Text = Format(nbEcoule / nbGames, "0") & " moves / game"
            lblGameDuration.Text = Trim(Format(totDuration / (nbGames - 1), "# ##0")) & " sec / game"
        End If

        lblConsecutive.Text = nbDrawScore & " / " & maxNul & " consecutive draws"
        lblFiftyMoves.Text = Format(50 - fiftyMoves) & " moves before draw"

        If nbMoves > 0 Then
            Me.Text = Me.Text & nbMoves
            lblMaxMoves.Text = Format(maxMoves - nbMoves) & " moves before draw"
        Else
            Me.Text = Me.Text & "0"
            lblMaxMoves.Text = maxMoves & " moves before draw"
        End If

        If nbEcoule = 1 Then
            lblEcouleCoup.Text = Trim(Format(totEcoule / nbEcoule, "## ##0")) & " ms / move"

            lblDepthServer.Text = "D" & Format(totDepthServer / nbEcoule, "0") & " (max. D" & maxDepthServer & ")"
            lblSpeedServer.Text = Trim(Format(totSpeedServer / nbEcoule, "# ### ##0")) & " Knps (max. " & Trim(Format(maxVitServeur, "# ### ##0")) & ")"

            lblDepthClient.Text = "D" & Format(totDepthClient / nbEcoule, "0") & " (max. D" & maxDepthClient & ")"
            lblSpeedClient.Text = Trim(Format(totSpeedClient / nbEcoule, "# ### ##0")) & " Knps (max. " & Trim(Format(maxVitClient, "# ### ##0")) & ")"

            If modePonder = "1" Then
                lblPredictionServer.Text = "Ponder @ " & Format(totPredictServer / nbEcoule, "0.0%")
                lblPredictionClient.Text = "Ponder @ " & Format(totPredictClient / nbEcoule, "0.0%")
            End If
        ElseIf nbEcoule > 1 Then
            lblEcouleCoup.Text = Trim(Format(totEcoule / nbEcoule, "## ##0")) & " ms / move"

            lblDepthServer.Text = "D" & Format(totDepthServer / Math.Round(nbEcoule / 2), "0") & " (max. D" & maxDepthServer & ")"
            lblSpeedServer.Text = Trim(Format(totSpeedServer / Math.Round(nbEcoule / 2), "# ### ##0")) & " Knps (max. " & Trim(Format(maxVitServeur, "# ### ##0")) & ")"

            lblDepthClient.Text = "D" & Format(totDepthClient / Math.Round(nbEcoule / 2), "0") & " (max. D" & maxDepthClient & ")"
            lblSpeedClient.Text = Trim(Format(totSpeedClient / Math.Round(nbEcoule / 2), "# ### ##0")) & " Knps (max. " & Trim(Format(maxVitClient, "# ### ##0")) & ")"

            If modePonder = "1" Then
                lblPredictionServer.Text = "Ponder @ " & Format(totPredictServer / Math.Round(nbEcoule / 2), "0.0%")
                lblPredictionClient.Text = "Ponder @ " & Format(totPredictClient / Math.Round(nbEcoule / 2), "0.0%")
            End If
        Else
            lblEcouleCoup.Text = "0 ms / move"

            lblDepthServer.Text = "D0 (max. D0)"
            lblSpeedServer.Text = "0 Knps (max. 0)"

            lblDepthClient.Text = "D0 (max. D0)"
            lblSpeedClient.Text = "0 Knps (max. 0)"

            lblPredictionServer.Text = "Ponder @ 0.0%"
            lblPredictionClient.Text = "Ponder @ 0.0%"
        End If

        horlogeBlanc(restantBlanc)
        horlogeNoir(restantNoir)

        If trait = "white" Then
            lblTraitBlanc.BackColor = Color.White
            lblWhiteClock.BorderStyle = BorderStyle.FixedSingle
            lblTraitNoir.BackColor = SystemColors.Control
            lblTraitNoir.ForeColor = SystemColors.ControlText
            lblBlackClock.BorderStyle = BorderStyle.None
        ElseIf trait = "black" Then
            lblTraitBlanc.BackColor = SystemColors.Control
            lblWhiteClock.BorderStyle = BorderStyle.None
            lblTraitNoir.BackColor = Color.Black
            lblTraitNoir.ForeColor = SystemColors.HighlightText
            lblBlackClock.BorderStyle = BorderStyle.FixedSingle
        End If

        If 0 < nbWhiteScores And nbWhiteScores < maxScore Then
            lblVictory.Text = "White victory in " & Format(maxScore - nbWhiteScores) & " moves"
        ElseIf 0 < nbBlackScores And nbBlackScores < maxScore Then
            lblVictory.Text = "Black victory in " & Format(maxScore - nbBlackScores) & " moves"
        ElseIf nbWhiteScores >= maxScore Then
            lblVictory.Text = "White victory !"
        ElseIf nbBlackScores >= maxScore Then
            lblVictory.Text = "Black victory !"
        Else
            lblVictory.Text = "No imminent victory"
        End If

        pbScore.Refresh()

        Application.DoEvents()
    End Sub

    Private Sub majEchange(Optional actionEchange As String = "", Optional coupFREchange As String = "", Optional coupMoteurEchange As String = "", Optional coupPonderEchange As String = "", Optional scoreEchange As String = "", Optional profondeurEchange As String = "", Optional ecouleEchange As String = "", Optional vitesseEchange As String = "")
        If actionEchange <> "" Then
            txtHeure.Text = Format(Now, "HH:mm:ss")
            txtAction.Text = actionEchange
        Else
            txtHeure.Text = ""
            txtAction.Text = ""
        End If
        txtBestmove.Text = coupsEN(coupFREchange)
        If coupFREchange <> "" Then
            txtBestmove.BackColor = Color.White
        End If
        txtMoveEngine.Text = coupMoteurEchange
        If modePonder = "0" Then
            txtCoupPonder.Text = ""
        ElseIf modePonder = "1" Then
            txtCoupPonder.Text = coupPonderEchange
        End If
        txtScore.Text = scoreEchange
        If profondeurEchange <> "" Then
            txtDepth.Text = "D" & profondeurEchange
        Else
            txtDepth.Text = ""
        End If
        If ecouleEchange <> "" Then
            txtEcoule.Text = Trim(Format(CInt(ecouleEchange), "## ##0 ms"))
        Else
            txtEcoule.Text = ecouleEchange
        End If
        If vitesseEchange <> "" Then
            txtSpeed.Text = Trim(Format(CInt(vitesseEchange), "### ##0 Knps"))
        Else
            txtSpeed.Text = vitesseEchange
        End If
        Application.DoEvents()
    End Sub

    Private Sub nettoyerLOG()
        'log
        If My.Computer.FileSystem.FileExists(My.Computer.Name & "_transmission.log") Then
            Try
                My.Computer.FileSystem.DeleteFile(My.Computer.Name & "_transmission.log")
            Catch ex As Exception

            End Try
        End If
        If My.Computer.FileSystem.FileExists(My.Computer.Name & "_reception.log") Then
            Try
                My.Computer.FileSystem.DeleteFile(My.Computer.Name & "_reception.log")
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub sauverParametres()
        Dim chaine As String

        chaine = txtDelay.Name & " = " & txtDelay.Text & vbCrLf
        chaine = chaine & txtIncrement.Name & " = " & txtIncrement.Text & vbCrLf
        chaine = chaine & txtThreads.Name & " = " & txtThreads.Text & vbCrLf
        chaine = chaine & txtHash.Name & " = " & txtHash.Text & vbCrLf
        chaine = chaine & txtEngine.Name & " = " & txtEngine.Text

        My.Computer.FileSystem.WriteAllText(My.Computer.Name & ".ini", chaine, False)

    End Sub

    Private Sub sauverStats()
        Dim chaine As String, fichier As String, chainePonder As String

        chaine = "roundPGN = " & roundPGN & vbCrLf
        chaine = chaine & "nbGames = " & Format(nbGames - 1) & vbCrLf
        chaine = chaine & "iFEN = " & iFEN & vbCrLf
        chaine = chaine & "whitePlayer = " & whitePlayer & vbCrLf
        chaine = chaine & "blackPlayer = " & blackPlayer & vbCrLf
        chaine = chaine & "whiteScore = " & Format(whiteScore, "0.0") & vbCrLf
        chaine = chaine & "blackScore = " & Format(blackScore, "0.0") & vbCrLf
        chaine = chaine & "serverScore = " & Format(serverScore, "0.0") & vbCrLf
        chaine = chaine & "clientScore = " & Format(clientScore, "0.0") & vbCrLf
        chaine = chaine & "drawGames = " & Format(drawGames, "0.0") & vbCrLf
        chaine = chaine & "nbEcoule = " & nbEcoule & vbCrLf
        chaine = chaine & "totEcoule = " & totEcoule & vbCrLf
        chaine = chaine & "totDepthServer = " & totDepthServer & vbCrLf
        chaine = chaine & "totDepthClient = " & totDepthClient & vbCrLf
        chaine = chaine & "totSpeedServer = " & totSpeedServer & vbCrLf
        chaine = chaine & "totSpeedClient = " & totSpeedClient & vbCrLf
        chaine = chaine & "totPredictServer = " & totPredictServer & vbCrLf
        chaine = chaine & "totPredictClient = " & totPredictClient & vbCrLf
        chaine = chaine & "totDuration = " & totDuration
        'chaine = chaine & "modePonder = " & modePonder

        chainePonder = ""
        If modePonder = "1" Then
            chainePonder = "ponder_on"
        ElseIf modePonder = "0" Then
            chainePonder = "ponder_off"
        End If

        fichier = "stats_" & My.Computer.Name & "_" _
                & txtDelay.Text & "sec_" & txtIncrement.Text & "ms_" _
                & txtThreads.Text & "C_" & txtHash.Text & "MB_" _
                & chainePonder

        My.Computer.FileSystem.WriteAllText(fichier & ".ini", chaine, False)

    End Sub

    Private Sub traitement(chaine As String, socket As Socket)
        Dim tabChaine() As String, coupFR As String, i As Integer, fen As String, min As Integer
        Dim tabPosition() As String, tabScore(0) As String, hote As String, hote_inv As String

        tabScore(0) = ""

        hote = ""
        hote_inv = ""
        If rbServer.Checked Then
            hote = "server"
            hote_inv = "client"
        ElseIf rbClient.Checked Then
            hote = "client"
            hote_inv = "server"
        End If

        tabChaine = Split(chaine, ":")
        chaine = ""

        nbMoves = Val(tabChaine(0)) 'on ne vérifie pas si on dépasse les 200 car l'adversaire a dû le faire

        moveEngine = tabChaine(1)
        eval = tabChaine(2)
        ecoule = Val(tabChaine(3))
        profondeur = Val(tabChaine(4))
        vitesse = Val(tabChaine(5))

        restantBlanc = Val(tabChaine(6)) 'on ne vérifie pas si l'adversaire a perdu au temps car il a dû le faire
        restantNoir = Val(tabChaine(7))
        increment = Val(tabChaine(8))

        fen = tabChaine(9)

        ponderAdverse = tabChaine(10) 'pour maj stat prediction

        If nbMoves > 0 Then
            If moveEngine = "..." Then
                'COUP MOTEUR ADVERSE
                coupFR = "1. " & moveEngine

                'SCORE ADVERSE
                eval = "0,00"
                score = score & eval & " "

                'ECOULE ADVERSE
                nbEcoule = nbEcoule + 1

                'plus assez de temps ?
                'rien à faire

                'PROFONDEUR/VITESSE ADVERSE
                'rien à faire

                'indexation
                positionFR = positionFR & coupFR & " "
            Else
                'COUP MOTEUR ADVERSE
                positionEngine = positionEngine & moveEngine & " "
                coupFR = analyseCoups(moveEngine, positionFR, fen)

                pbEchiquier.Refresh()

                'repetition ?
                'déjà vérifier par l'adversaire

                'règle des 50 coups
                'c'est juste pour majAffichage car l'adversaire a dû vérifier s'il y avait une égalité
                If coupFR = "0-0" Or coupFR = "0-0-0" Or (InStr(coupFR, "x", CompareMethod.Text) = 0 And InStr("CDFRT", gauche(coupFR, 1)) > 0) Then
                    fiftyMoves = fiftyMoves + 1
                Else
                    fiftyMoves = 0
                End If

                'SCORE ADVERSE
                score = score & eval & " "
                tabScore = Split(Trim(score), " ")
                'compter les scores entre -0.01 et 0.01
                'c'est juste pour majAffichage car l'adversaire a dû vérifier s'il y avait une victoire
                nbDrawScore = 0
                If nbMoves < maxNul Then
                    min = 0
                Else
                    min = tabScore.Length - maxNul
                End If
                For i = UBound(tabScore) To min Step -1
                    If -0.01 <= CSng(tabScore(i)) And CSng(tabScore(i)) <= 0.01 Then
                        nbDrawScore = nbDrawScore + 1
                    Else
                        Exit For
                    End If
                    Application.DoEvents()
                Next
                'compter les scores > seuilScore et < seuilScore
                'c'est juste pour majAffichage car l'adversaire a dû vérifier s'il y avait une victoire
                nbWhiteScores = 0
                nbBlackScores = 0
                If nbMoves < maxScore Then
                    min = 0
                Else
                    min = tabScore.Length - maxScore
                End If
                For i = UBound(tabScore) To min Step -1
                    If (i Mod 2 = 0 And seuilScore <= CSng(tabScore(i))) _
                    Or (i Mod 2 = 1 And CSng(tabScore(i)) <= -seuilScore) Then
                        nbWhiteScores = nbWhiteScores + 1
                    ElseIf (i Mod 2 = 0 And CSng(tabScore(i)) <= -seuilScore) _
                        Or (i Mod 2 = 1 And seuilScore <= CSng(tabScore(i))) Then
                        nbBlackScores = nbBlackScores + 1
                    End If
                    Application.DoEvents()
                Next

                'ECOULE ADVERSE
                totEcoule = totEcoule + ecoule
                nbEcoule = nbEcoule + 1

                'plus assez de temps ?
                'déjà vérifier par l'adversaire
                If trait = "white" Then
                    lblTraitBlanc.Text = Format(ecoule, "## ##0") & " ms"
                ElseIf trait = "black" Then
                    'on met à jour l'horloge des noirs
                    lblTraitNoir.Text = Format(ecoule, "## ##0") & " ms"
                End If

                'PROFONDEUR/VITESSE ADVERSE
                If rbServer.Checked Then
                    totDepthClient = totDepthClient + profondeur
                    If profondeur > maxDepthClient Then
                        maxDepthClient = profondeur
                    End If

                    totSpeedClient = totSpeedClient + vitesse
                    If vitesse > maxVitClient Then
                        maxVitClient = vitesse
                    End If
                ElseIf rbClient.Checked Then
                    totDepthServer = totDepthServer + profondeur
                    If profondeur > maxDepthServer Then
                        maxDepthServer = profondeur
                    End If

                    totSpeedServer = totSpeedServer + vitesse
                    If vitesse > maxVitServeur Then
                        maxVitServeur = vitesse
                    End If
                End If

                'ponderMoteur contient le coup que l'on attendait de l'adversaire
                'coupMoteur contient le coup adverse
                If ponderMoteur <> "" And moveEngine <> "" Then
                    If ponderMoteur = moveEngine Then
                        If rbServer.Checked Then
                            totPredictServer = totPredictServer + 1
                        ElseIf rbClient.Checked Then
                            totPredictClient = totPredictClient + 1
                        End If
                    End If
                End If

                'indexation
                If nbMoves Mod 2 = 1 Then
                    coupFR = formaterCoups("pgn", coupFR, nbMoves)
                End If

                '+ si score positif
                chaine = ""
                If gauche(eval, 1) <> "-" Then
                    chaine = "+"
                End If
                positionFR = positionFR & coupFR & " {" & chaine & Replace(eval, ",", ".") & "/" & profondeur & " " & Int(ecoule / 1000) & "} "

                'affichage score du point de vue des blancs
                'chaine = Format(Now, "HH:mm:ss") & " le " & hote_inv & " (" & trait & ") a joué : " & coupFR & " (" & moveEngine & ", ponder " & ponderAdverse & ", "
                If trait = "white" Then
                    'chaine = chaine & eval
                    chaine = eval
                ElseIf trait = "black" Then
                    'chaine = chaine & Format(-CSng(eval), "0.00")
                    chaine = Format(-CSng(eval), "0.00")
                End If
                'chaine = chaine & ", D" & profondeur & ", " & ecoule & " ms, " & vitesse & " Knps)"
                'lstMessage.Items.Add(chaine)
                'lstMessage.SelectedIndex = lstMessage.Items.Count - 1
                'Application.DoEvents()
                If modePonder = "1" Then
                    If moveEngine <> "" And ponderMoteur <> "" And ponderMoteur = moveEngine Then
                        txtBestmove.BackColor = Color.LightGreen
                    Else
                        txtBestmove.BackColor = Color.White
                    End If
                End If
                majEchange("the " & hote_inv & " (" & trait & ") played", coupFR, moveEngine, ponderAdverse, chaine, profondeur, ecoule, vitesse)
            End If

            'on change de trait au dernier moment possible
            If trait = "white" Then
                trait = "black"
            Else
                trait = "white"
            End If
        End If

        majAffichage()

        lstMessage.Items.Add(Format(Now, "HH:mm:ss") & " analysis in progress...")
        lstMessage.SelectedIndex = lstMessage.Items.Count - 1
        Application.DoEvents()
        eval = "0,00"
        profondeur = 0
        vitesse = 0
        ponderMoteur = ""
        'on récupère le coup moteur, le score, la profondeur et le temps écoulé
        moveEngine = analyse(positionEngine, restantBlanc, restantNoir, increment, fen)
        analyseEnCours = False

        If Not crash Then

            nbMoves = nbMoves + 1

            'COUP MOTEUR
            positionEngine = positionEngine & moveEngine & " "
            coupFR = analyseCoups(moveEngine, positionFR, fen)

            pbEchiquier.Refresh()

            'repetition ?
            repetition = False
            tabPosition = Split(Trim(positionEngine), " ")
            If nbMoves > 9 Then
                If tabPosition(UBound(tabPosition)) = tabPosition(UBound(tabPosition) - 4) And tabPosition(UBound(tabPosition) - 4) = tabPosition(UBound(tabPosition) - 8) _
                    And tabPosition(UBound(tabPosition) - 1) = tabPosition(UBound(tabPosition) - 5) Then
                    repetition = True
                End If
            End If

            'règle des 50 coups
            If coupFR = "0-0" Or coupFR = "0-0-0" Or (InStr(coupFR, "x", CompareMethod.Text) = 0 And InStr("CDFRT", gauche(coupFR, 1)) > 0) Then
                fiftyMoves = fiftyMoves + 1
            Else
                fiftyMoves = 0
            End If

            'SCORE
            If IsNumeric(eval) And eval <> "" Then
                score = score & eval & " "
                tabScore = Split(Trim(score), " ")
                'compter les scores entre -0.01 et 0.01
                nbDrawScore = 0
                If nbMoves < maxNul Then
                    min = 0
                Else
                    min = tabScore.Length - maxNul
                End If
                For i = UBound(tabScore) To min Step -1
                    If -0.01 <= CSng(tabScore(i)) And CSng(tabScore(i)) <= 0.01 Then
                        nbDrawScore = nbDrawScore + 1
                    Else
                        Exit For
                    End If
                    Application.DoEvents()
                Next
                'compter les scores > seuilScore et < seuilScore
                nbWhiteScores = 0
                nbBlackScores = 0
                If nbMoves < maxScore Then
                    min = 0
                Else
                    min = tabScore.Length - maxScore
                End If
                For i = UBound(tabScore) To min Step -1
                    If (i Mod 2 = 0 And seuilScore <= CSng(tabScore(i))) _
                    Or (i Mod 2 = 1 And CSng(tabScore(i)) <= -seuilScore) Then
                        nbWhiteScores = nbWhiteScores + 1
                        If nbWhiteScores >= maxScore Then
                            victoireBlanc = True
                            Exit For
                        End If
                    ElseIf (i Mod 2 = 0 And CSng(tabScore(i)) <= -seuilScore) _
                    Or (i Mod 2 = 1 And seuilScore <= CSng(tabScore(i))) Then
                        nbBlackScores = nbBlackScores + 1
                        If nbBlackScores >= maxScore Then
                            victoireNoir = True
                            Exit For
                        End If
                    End If
                    Application.DoEvents()
                Next
            ElseIf eval = "mate" Or eval = "victory" Then
                score = score & eval
            End If

            If Not mate And Not victoireNoir And Not victoireBlanc Then
                'ECOULE
                totEcoule = totEcoule + ecoule
                nbEcoule = nbEcoule + 1

                'plus assez de temps ?
                If trait = "white" Then
                    'on met à jour l'horloge des blancs
                    restantBlanc = restantBlanc - ecoule + increment
                    lblTraitBlanc.Text = Format(ecoule, "## ##0") & " ms"
                    If restantBlanc < -increment Then
                        victoireNoir = True
                    End If
                ElseIf trait = "black" Then
                    'on met à jour l'horloge des noirs
                    restantNoir = restantNoir - ecoule + increment
                    lblTraitNoir.Text = Format(ecoule, "## ##0") & " ms"
                    If restantNoir < -increment Then
                        victoireBlanc = True
                    End If
                End If

                'PROFONDEUR/VITESSE
                If rbServer.Checked Then
                    totDepthServer = totDepthServer + profondeur
                    If profondeur > maxDepthServer Then
                        maxDepthServer = profondeur
                    End If

                    totSpeedServer = totSpeedServer + vitesse 'Knps
                    If vitesse > maxVitServeur Then
                        maxVitServeur = vitesse
                    End If
                ElseIf rbClient.Checked Then
                    totDepthClient = totDepthClient + profondeur
                    If profondeur > maxDepthClient Then
                        maxDepthClient = profondeur
                    End If

                    totSpeedClient = totSpeedClient + vitesse 'Knps
                    If vitesse > maxVitClient Then
                        maxVitClient = vitesse
                    End If
                End If

                'ponderAdverse contient le coup que l'adversaire attendait de nous
                'coupMoteur contient notre coup
                If ponderAdverse <> "" And moveEngine <> "" Then
                    If ponderAdverse = moveEngine Then
                        If rbServer.Checked Then
                            totPredictClient = totPredictClient + 1
                        ElseIf rbClient.Checked Then
                            totPredictServer = totPredictServer + 1
                        End If
                    End If
                End If

                'indexation
                If nbMoves Mod 2 = 1 Then
                    coupFR = formaterCoups("pgn", coupFR, nbMoves)
                End If

                '+ si score positif
                chaine = ""
                If gauche(eval, 1) <> "-" Then
                    chaine = "+"
                End If
                positionFR = positionFR & coupFR & " {" & chaine & Replace(eval, ",", ".") & "/" & profondeur & " " & Int(ecoule / 1000) & "} "

                'affichage score du point de vue des blancs
                'chaine = Format(Now, "HH:mm:ss") & " le " & hote & " (" & trait & ") joue : " & coupFR & " (" & coupMoteur & ", ponder " & ponderMoteur & ", "
                If trait = "white" Then
                    'chaine = chaine & eval
                    chaine = eval
                ElseIf trait = "black" Then
                    'chaine = chaine & Format(-CSng(eval), "0.00")
                    chaine = Format(-CSng(eval), "0.00")
                End If
                'chaine = chaine & ", D" & profondeur & ", " & ecoule & " ms, " & vitesse & " Knps)"
                'lstMessage.Items.Add(chaine)
                If modePonder = "1" Then
                    If moveEngine <> "" And ponderAdverse <> "" And ponderAdverse = moveEngine Then
                        txtBestmove.BackColor = Color.Orange
                    Else
                        txtBestmove.BackColor = Color.White
                    End If
                End If
                majEchange("the " & hote & " (" & trait & ") plays", coupFR, moveEngine, ponderMoteur, chaine, profondeur, ecoule, vitesse)
                lstMessage.Items.Add(Format(Now, "HH:mm:ss") & " waiting for the opponent's move...")
                lstMessage.Items.Add(" ")
                lstMessage.SelectedIndex = lstMessage.Items.Count - 1
                Application.DoEvents()
            End If
        End If

        majAffichage()

        'fin de partie ?
        If Not crash And Not mate And Not victoireNoir And Not victoireBlanc _
        And Not repetition And nbMoves <= maxMoves And nbDrawScore < maxNul And fiftyMoves < 50 Then
            'on transmet les infos à l'adversaire
            chaine = nbMoves & ":" & moveEngine & ":" & eval & ":" & ecoule & ":" & profondeur & ":" & vitesse & ":" & restantBlanc & ":" & restantNoir & ":" & increment & ":" & fen & ":" & ponderMoteur

            While pause And modePonder = "0"
                Application.DoEvents()
                Threading.Thread.Sleep(1000)
            End While

            transmission(chaine, socket)
            If trait = "white" Then
                trait = "black"
            Else
                trait = "white"
            End If
        Else
            'on arrête
            timerCommunication.Enabled = False

            If Not crash Then
                arretMoteur()

                nbGames = nbGames + 1

                If mate Then
                    positionFR = Trim(positionFR) & "#"
                    If nbMoves Mod 2 = 1 Then
                        whiteScore = whiteScore + 1
                        If InStr(whitePlayer, " (server)", CompareMethod.Text) > 0 Then
                            serverScore = serverScore + 1
                        ElseIf InStr(whitePlayer, " (client)", CompareMethod.Text) > 0 Then
                            clientScore = clientScore + 1
                        End If
                    ElseIf nbMoves Mod 2 = 0 Then
                        blackScore = blackScore + 1
                        If InStr(blackPlayer, " (server)", CompareMethod.Text) > 0 Then
                            serverScore = serverScore + 1
                        ElseIf InStr(blackPlayer, " (client)", CompareMethod.Text) > 0 Then
                            clientScore = clientScore + 1
                        End If
                    End If
                ElseIf victoireBlanc Then
                    positionFR = positionFR & "1-0"
                    whiteScore = whiteScore + 1
                    If InStr(whitePlayer, " (server)", CompareMethod.Text) > 0 Then
                        serverScore = serverScore + 1
                    ElseIf InStr(whitePlayer, " (client)", CompareMethod.Text) > 0 Then
                        clientScore = clientScore + 1
                    End If
                ElseIf victoireNoir Then
                    positionFR = positionFR & "0-1"
                    blackScore = blackScore + 1
                    If InStr(blackPlayer, " (server)", CompareMethod.Text) > 0 Then
                        serverScore = serverScore + 1
                    ElseIf InStr(blackPlayer, " (client)", CompareMethod.Text) > 0 Then
                        clientScore = clientScore + 1
                    End If
                ElseIf repetition Or nbDrawScore >= maxNul Or nbMoves > maxMoves Or fiftyMoves >= 50 Then
                    positionFR = positionFR & "1/2-1/2"
                    whiteScore = whiteScore + 0.5
                    blackScore = blackScore + 0.5
                    serverScore = serverScore + 0.5
                    clientScore = clientScore + 0.5
                    drawGames = drawGames + 1
                End If

                endGame = Now.Ticks / 10000
                totDuration = totDuration + (endGame - gameStart) / 1000

                'on prévient l'adversaire
                transmission("stop" & ":" & positionFR & ":" & whiteScore & ":" & blackScore & ":" & serverScore & ":" & clientScore & ":" & drawGames & ":" & endGame, socket)
                If rbServer.Checked Then
                    sauverStats()
                    My.Computer.FileSystem.WriteAllText("check.pgn", structurePGN(coupsEN(positionFR), fen, whitePlayer, blackPlayer, sitePGN, roundPGN), True)
                Else
                    whitePlayer = ""
                    blackPlayer = ""
                End If

                initialisation()
            End If

        End If
    End Sub

    Private Sub verification()
        Dim maxThreads As Integer, maxHash As Integer

        maxThreads = cpu()
        lblMaxThreads.Text = "/" & maxThreads

        maxHash = Math.Round(My.Computer.Info.TotalPhysicalMemory / 1024 / 1024 / 1024 / 2) * 1024
        If Val(txtThreads.Text) > maxThreads Then
            txtThreads.Text = maxThreads
        End If

        If Val(txtHash.Text) > maxHash Then 'max = ram / 2
            txtHash.Text = maxHash
        End If
    End Sub

    Private Sub pbScore_Paint(sender As Object, e As PaintEventArgs) Handles pbScore.Paint
        Dim tabScore() As String, i As Integer, val As Single, val_prec As Single
        Dim pasX As Integer, pasY As Integer, offset_vertical As Integer, hauteur_dispo As Integer
        
        If score = "" Then
            Exit Sub
        End If

        e.Graphics.Clear(Color.LightGray)

        tabScore = Split(score, " ")

        hauteur_dispo = sender.height

        offset_vertical = hauteur_dispo / 2

        pasX = sender.Width / nbMoves
        pasY = hauteur_dispo / 6

        val_prec = 0
        For i = 0 To UBound(tabScore)
            If IsNumeric(tabScore(i)) Then

                If i Mod 2 = 0 Then
                    'coup blanc
                    val = CSng(tabScore(i))
                ElseIf i Mod 2 = 1 Then
                    'coup noir
                    val = -CSng(tabScore(i))
                End If

                If val >= 3 Then
                    val = 3
                ElseIf val <= -3 Then
                    val = -3
                End If

                Dim point1 As New Point(i * pasX, offset_vertical) 'x, 0
                Dim point2 As New Point(i * pasX, offset_vertical - val_prec * pasY) 'x, y
                Dim point3 As New Point((i + 1) * pasX, offset_vertical - val * pasY) 'x1, y1
                Dim point4 As New Point((i + 1) * pasX, offset_vertical) 'x1, 0
                Dim tabPoints As Point() = {point1, point2, point3, point4}

                'remplissage
                If val = 0 Then
                    If val_prec >= 0 Then
                        e.Graphics.FillPolygon(Brushes.White, tabPoints)
                    ElseIf val_prec < 0 Then
                        e.Graphics.FillPolygon(Brushes.Black, tabPoints)
                    End If
                ElseIf val > 0 Then
                    e.Graphics.FillPolygon(Brushes.White, tabPoints)
                ElseIf val < 0 Then
                    e.Graphics.FillPolygon(Brushes.Black, tabPoints)
                End If

                'tiret
                If i Mod 2 = 0 Then
                    e.Graphics.DrawLine(Pens.Green, i * pasX, hauteur_dispo, i * pasX, 0)
                End If

                val_prec = val
            End If
            Application.DoEvents()
        Next

        'grille
        e.Graphics.DrawLine(Pens.Green, 0, offset_vertical - 2 * pasY, sender.width, offset_vertical - 2 * pasY) '2.00
        e.Graphics.DrawLine(Pens.Green, 0, offset_vertical - pasY, sender.width, offset_vertical - pasY) '1.00
        e.Graphics.DrawLine(Pens.Green, 0, offset_vertical, sender.width, offset_vertical) '0.00
        e.Graphics.DrawLine(Pens.Green, 0, offset_vertical + pasY, sender.width, offset_vertical + pasY) '-1.00
        e.Graphics.DrawLine(Pens.Green, 0, offset_vertical + 2 * pasY, sender.width, offset_vertical + 2 * pasY) '-2.00

    End Sub

    Private Sub pbEchiquier_Paint(sender As Object, e As PaintEventArgs) Handles pbEchiquier.Paint
        If txtFEN.Text = "" Then
            chaineEPD = moteurEPD("engine.exe", positionEngine)
        Else
            chaineEPD = moteurEPD("engine.exe", positionEngine, txtFEN.Text)
        End If

        'ne plus afficher le ponder blanc quand le noir vient de jouer
        If (My.Computer.Name & " (" & modeReseau & ")" = whitePlayer And trait = "black") _
        Or (My.Computer.Name & " (" & modeReseau & ")" = blackPlayer And trait = "white") Then
            'on cherche à afficher en rouge le coup attendu par l'adversaire
            echiquier(chaineEPD, e, ponderAdverse, ponderAdverse)
        Else
            echiquier(chaineEPD, e, "", ponderMoteur)
        End If
        pbMateriel.Refresh()
    End Sub

    Private Sub pbMateriel_Paint(sender As Object, e As PaintEventArgs) Handles pbMateriel.Paint
        echiquier_differences(chaineEPD, e)
    End Sub

End Class
