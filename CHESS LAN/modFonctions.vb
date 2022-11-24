Imports System.Drawing.Imaging
Imports System.Management 'ajouter référence system.management
Imports System.Net
Imports System.Net.Sockets
Imports VB = Microsoft.VisualBasic

Module modFonctions

    'reseau
    Public socketConnexion As Socket
    Public socketMessage As Socket
    Private Const finMessage = ":#$!%*_&@?"
    Public modeReseau As String

    'moteur
    Private moteur As New System.Diagnostics.Process()
    Private entreeMoteur As System.IO.StreamWriter
    Private erreurMoteur As System.IO.StreamReader
    Private sortie As String
    Public crash As Boolean

    'ponder
    Public modePonder As String
    Public ponderMoteur As String
    Public ponderAdverse As String
    Public positionPonder As String

    'horloge
    Public depart As Long
    Public ecoule As Integer
    Public analyseEnCours As Boolean
    Public trait As String
    Public memRestant As Integer

    'fin de partie
    Public eval As String
    Public mate As Boolean
    Public score As String
    Public victoireNoir As Boolean
    Public victoireBlanc As Boolean

    'statistiques
    Public profondeur As Integer
    Public vitesse As Integer

    Public Function adresseIPv4(hote As String) As String
        Dim carte As IPAddress

        For Each carte In Dns.GetHostEntry(hote).AddressList
            If carte.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                Return carte.ToString
            End If
        Next

        Return hote
    End Function

    Public Function analyse(coups As String, blanc As Integer, noir As Integer, inc As Integer, fen As String) As String
        Dim chaine As String, tabSortie() As String, i As Integer, tmp As String, tabTmp() As String

        analyseEnCours = True
        crash = False

        If modePonder = "1" Then
            If coups <> "" And positionPonder <> "" Then
                'si l'adversaire a joué le coup attendu => ponderhits
                If coups = positionPonder Then
                    depart = Now.Ticks / 10000
                    entreeMoteur.WriteLine("ponderhit")
                    GoTo ponder_ok
                Else
                    'si l'adversaire n'a pas joué le coup attendu => stop
                    entreeMoteur.WriteLine("stop")
                    While InStr(sortie, "bestmove", CompareMethod.Text) = 0
                        Application.DoEvents()
                        Threading.Thread.Sleep(10)
                    End While
                End If
            End If
        End If

        positionnerMoteur(coups, fen)
        Threading.Thread.Sleep(100)
        sortie = ""

        depart = Now.Ticks / 10000
        entreeMoteur.WriteLine("go wtime " & blanc & " btime " & noir & " winc " & inc & " binc " & inc)

ponder_ok:
        While InStr(sortie, "bestmove", CompareMethod.Text) = 0
            Application.DoEvents()
            Threading.Thread.Sleep(10)

            If (Now.Ticks / 10000 - depart) > memRestant Then
                crash = True
                Return ""
            End If
        End While
        ecoule = Now.Ticks / 10000 - depart

        tabSortie = Split(sortie, vbCrLf)

        chaine = ""
        For i = UBound(tabSortie) To 0 Step -1
            If tabSortie(i) <> "" Then
                'meilleur coup
                If InStr(tabSortie(i), "bestmove", CompareMethod.Text) > 0 And chaine = "" Then
                    tabTmp = Split(tabSortie(i), " ")
                    chaine = tabTmp(1)
                    If InStr(chaine, " ", CompareMethod.Text) > 0 Then
                        chaine = chaine.Substring(0, chaine.IndexOf(" "))
                    ElseIf InStr(chaine, vbCrLf) > 0 Then
                        chaine = chaine.Substring(0, chaine.IndexOf(vbCrLf))
                    End If
                    If modePonder = "1" Then
                        Try
                            ponderMoteur = tabTmp(3)
                        Catch ex As Exception

                        End Try
                    End If
                End If

                If chaine <> "" And InStr(tabSortie(i), " pv " & chaine) > 0 Then
                    'profondeur
                    If InStr(tabSortie(i), " depth ", CompareMethod.Text) > 0 Then
                        profondeur = Val(Trim(tabSortie(i).Substring(tabSortie(i).IndexOf(" depth ") + 7, 3)))
                    End If

                    'vitesse
                    If InStr(tabSortie(i), " nps ", CompareMethod.Text) > 0 Then
                        tmp = tabSortie(i).Substring(tabSortie(i).LastIndexOf(" nps ") + 5)
                        If InStr(tmp, " ", CompareMethod.Text) > 1 Then
                            tmp = gauche(tmp, tmp.IndexOf(" "))
                        End If
                        vitesse = Val(tmp) / 1000
                    End If

                    'score
                    If InStr(tabSortie(i), " score cp ", CompareMethod.Text) > 0 Then
                        If tabSortie(i).LastIndexOf(" score cp ") + 10 <= Len(tabSortie(i)) Then
                            eval = tabSortie(i).Substring(tabSortie(i).LastIndexOf(" score cp ") + 10)
                            If InStr(eval, " ", CompareMethod.Text) > 1 Then
                                eval = gauche(eval, eval.IndexOf(" "))
                            End If
                            eval = Format(CSng(eval) / 100, "0.00")
                            Exit For
                        End If
                    ElseIf InStr(tabSortie(i), " score mate 1 ", CompareMethod.Text) > 0 Then
                        chaine = chaine & "#"
                        If eval = "" Then
                            eval = "mate"
                        End If
                        mate = True
                        Exit For
                    ElseIf InStr(tabSortie(i), " score mate ", CompareMethod.Text) > 0 And InStr(tabSortie(i), " score mate -", CompareMethod.Text) = 0 Then
                        If eval = "" Then
                            eval = "victory"
                        End If
                        If trait = "white" Then
                            victoireBlanc = True
                        ElseIf trait = "black" Then
                            victoireNoir = True
                        End If
                        Exit For
                    ElseIf InStr(tabSortie(i), " score mate -", CompareMethod.Text) > 0 Then
                        If eval = "" Then
                            eval = "victory"
                        End If
                        If trait = "white" Then
                            victoireNoir = True
                        ElseIf trait = "black" Then
                            victoireBlanc = True
                        End If
                        Exit For
                    Else
                        Application.DoEvents()
                        MsgBox("on fait quoi ?")
                    End If
                End If
                tabSortie(i) = ""
            End If
            Application.DoEvents()
        Next
        sortie = ""

        If modePonder = "1" Then
            If ponderMoteur <> "" And ponderMoteur <> "0000" Then
                If coups = "" Then
                    positionPonder = chaine & " " & ponderMoteur & " "
                Else
                    positionPonder = coups & chaine & " " & ponderMoteur & " "
                End If
                positionnerMoteur(positionPonder, fen)
                entreeMoteur.WriteLine("go ponder wtime " & blanc & " btime " & noir & " winc " & inc & " binc " & inc)
            Else
                positionPonder = ""
                ponderMoteur = ""
            End If
        End If

        Return chaine
    End Function

    Public Function analyseCoups(coupsEN As String, pgnFR As String, Optional epd As String = "") As String
        Dim tabCoups() As String, i As Integer, pieceCoups As String, separateur As String, promotion As String
        Dim chaine As String, departCoups As String, arriveeCoups As String, partie As String
        Dim departChaine As String, arriveeChaine As String, pieceChaine As String, chaineCoups As String
        Dim casesOccupees As String, prisePassant As String, coups As String, prefixFR As String

        chaineCoups = ""
        casesOccupees = "a1;b1;c1;d1;e1;f1;g1;h1;a2;b2;c2;d2;e2;f2;g2;h2;a7;b7;c7;d7;e7;f7;g7;h7;a8;b8;c8;d8;e8;f8;g8;h8;"
        prefixFR = "Ta1-a1 Cb1-b1 Fc1-c1 Dd1-d1 Re1-e1 Ff1-f1 Cg1-g1 Th1-h1 a2-a2 b2-b2 c2-c2 d2-d2 e2-e2 f2-f2 g2-g2 h2-h2 a7-a7 b7-b7 c7-c7 d7-d7 e7-e7 f7-f7 g7-g7 h7-h7 Ta8-a8 Cb8-b8 Fc8-c8 Dd8-d8 Re8-e8 Ff8-f8 Cg8-g8 Th8-h8 "
        If epd <> "" Then
            tabCoups = Split(epdCasesOccupees(epd), ":")
            casesOccupees = tabCoups(0)
            prefixFR = tabCoups(1)
        End If
        prisePassant = "a5xb6 b5xa6 b5xc6 c5xb6 c5xd6 d5xc6 d5xe6 e5xd6 e5xf6 f5xe6 f5xg6 g5xf6 g5xh6 h5xg6"
        prisePassant = prisePassant & " " & "a4xb3 b4xa3 b4xc3 c4xb3 c4xd3 d4xc3 d4xe3 e4xd3 e4xf3 f4xe3 f4xg3 g4xf3 g4xh3 h4xg3"
        pieceCoups = ""
        separateur = ""
        departCoups = ""
        arriveeCoups = ""
        departChaine = ""
        arriveeChaine = ""
        pieceChaine = ""
        promotion = ""

        'on dégage les commentaires
        partie = formaterCoups("", prefixFR & pgnFR)

        'on convertit les roques
        If InStr(partie, ". 0-0 ", CompareMethod.Text) > 0 Then
            partie = Replace(partie, ". 0-0 ", ". Re1-g1 Th1-f1 ")
        ElseIf InStr(partie, ". 0-0-0 ", CompareMethod.Text) > 0 Then
            partie = Replace(partie, ". 0-0-0 ", ". Re1-c1 Ta1-d1 ")
        End If
        If InStr(partie, " 0-0 ", CompareMethod.Text) > 0 Then
            partie = Replace(partie, " 0-0 ", " Re8-g8 Th8-f8 ")
        ElseIf InStr(partie, " 0-0-0 ", CompareMethod.Text) > 0 Then
            partie = Replace(partie, " 0-0-0 ", " Re8-c8 Ta8-d8 ")
        End If

        'on convertit les promotions
        'h2-h4 f7-f6 h4-h5 f6-f5 h5-h6 f5-f4 h6xg7 f4-f3 g7xh8=D Ff8-g7
        'g7xh8=D => g7xh8 + Dh8-h8
        While InStr(partie, "=", CompareMethod.Text) > 0
            partie = Replace(partie, partie.Substring(partie.IndexOf("=") - 2, 4), partie.Substring(partie.IndexOf("=") - 2, 2) & " " & partie.Substring(partie.IndexOf("=") + 1, 1) & partie.Substring(partie.IndexOf("=") - 2, 2) & "-" & partie.Substring(partie.IndexOf("=") - 2, 2))
            Application.DoEvents()
        End While

        'on cherche les infos du coups
        coups = formaterCoups("moteur", coupsEN)
        departCoups = gauche(coups, 2)
        If Len(coups) < 5 Then
            arriveeCoups = droite(coups, 2)
        Else
            Select Case droite(coups, 1)
                Case "q", "Q"
                    promotion = "=D"
                Case "r", "R"
                    promotion = "=T"
                Case "b", "B"
                    promotion = "=F"
                Case "n", "N"
                    promotion = "=C"
            End Select
            arriveeCoups = droite(gauche(coups, 4), 2)
        End If

        'on met à jour les cases occupées
        tabCoups = Split(partie, " ")
        For i = 0 To UBound(tabCoups)
            If tabCoups(i) <> "" And InStr(tabCoups(i), ".", CompareMethod.Text) = 0 And InStr(tabCoups(i), "{", CompareMethod.Text) = 0 And InStr(tabCoups(i), "}", CompareMethod.Text) = 0 And InStr(tabCoups(i), "/", CompareMethod.Text) = 0 _
            And InStr(tabCoups(i), "*", CompareMethod.Text) = 0 And InStr(tabCoups(i), "1-0", CompareMethod.Text) = 0 And InStr(tabCoups(i), "0-1", CompareMethod.Text) = 0 And InStr(tabCoups(i), "1/2-1/2", CompareMethod.Text) = 0 Then
                'si on a une prise en passant
                If InStr(tabCoups(i), "ep", CompareMethod.Text) > 0 Then
                    'on efface la case d'arrivée du coups précédent (pion qui avance de 2 cases)
                    casesOccupees = Replace(casesOccupees, arriveeChaine & ";", "")
                End If
                chaine = formaterCoups("moteur", tabCoups(i))

                departChaine = gauche(chaine, 2)
                arriveeChaine = droite(chaine, 2)

                casesOccupees = Replace(casesOccupees, departChaine & ";", "")
                If InStr(casesOccupees, arriveeChaine & ";") = 0 Then
                    casesOccupees = casesOccupees & arriveeChaine & ";"
                End If

                casesOccupees = trierChaine(casesOccupees, ";") & ";"

            End If
            Application.DoEvents()
        Next

        departChaine = ""
        arriveeChaine = ""

        'on analyse le coups
        For i = UBound(tabCoups) To 0 Step -1
            If tabCoups(i) <> "" And InStr(tabCoups(i), ".", CompareMethod.Text) = 0 And InStr(tabCoups(i), "{", CompareMethod.Text) = 0 And InStr(tabCoups(i), "}", CompareMethod.Text) = 0 And InStr(tabCoups(i), "/", CompareMethod.Text) = 0 _
            And InStr(tabCoups(i), "*", CompareMethod.Text) = 0 And InStr(tabCoups(i), "1-0", CompareMethod.Text) = 0 And InStr(tabCoups(i), "0-1", CompareMethod.Text) = 0 And InStr(tabCoups(i), "1/2-1/2", CompareMethod.Text) = 0 Then
                pieceChaine = ""

                chaine = tabCoups(i)
                If Len(tabCoups(i)) = 6 Then
                    chaine = droite(tabCoups(i), 5)
                    pieceChaine = gauche(tabCoups(i), 1)
                End If

                departChaine = gauche(chaine, 2)
                arriveeChaine = droite(chaine, 2)

                'case de départ connue ?
                If pieceCoups = "" And departCoups = arriveeChaine Then
                    If pieceChaine = "" Then
                        pieceCoups = "p"
                    Else
                        pieceCoups = pieceChaine
                    End If
                End If

                'case d'arrivée connue ?
                If separateur = "" And InStr(casesOccupees, arriveeCoups & ";") > 0 Then
                    separateur = "x"
                End If
            End If
        Next

        If separateur = "" Then
            separateur = "-"
        End If

        chaineCoups = Replace(pieceCoups, "p", "") & departCoups & separateur & arriveeCoups & promotion

        'roque
        If chaineCoups = "Re1-g1" Or chaineCoups = "Re8-g8" Then
            chaineCoups = "0-0"
        ElseIf chaineCoups = "Re1-c1" Or chaineCoups = "Re8-c8" Then
            chaineCoups = "0-0-0"
        End If

        'prise en passant
        If InStr(prisePassant, Replace(chaineCoups, "-", "x")) > 0 Then
            chaineCoups = Replace(chaineCoups, "-", "x")
        End If

        Return chaineCoups
    End Function

    Public Sub arretMoteur()
        Try
            entreeMoteur.WriteLine("stop")
            entreeMoteur.WriteLine("quit")
            entreeMoteur.Close()
            erreurMoteur.Close()
            moteur.CancelOutputRead()
            moteur.Close()
        Catch ex As Exception

        End Try
    End Sub

    Public Function binLong(suite As String) As Long
        Dim i As Integer, cumul As Long

        cumul = 0
        For i = 1 To Len(suite)
            cumul = cumul + CInt(gauche(droite(suite, i), 1)) * 2 ^ (i - 1)
        Next
        Return cumul

    End Function

    Public Sub chargerMoteur(cheminMoteur As String, puissance As String, memoire As String)
        Dim i As Integer, chaine As String, tabChaine() As String, optionsMoteur As String
        Dim affinite1 As Long, affinite2 As Long, coeurs As Integer

        sortie = ""

        'chargement moteur
        moteur.StartInfo.UseShellExecute = False
        moteur.StartInfo.RedirectStandardOutput = True
        AddHandler moteur.OutputDataReceived, AddressOf evenement
        moteur.StartInfo.RedirectStandardInput = True
        moteur.StartInfo.RedirectStandardError = True
        moteur.StartInfo.CreateNoWindow = True
        If My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.GetParentPath(cheminMoteur)) Then
            moteur.StartInfo.WorkingDirectory = My.Computer.FileSystem.GetParentPath(cheminMoteur)
        Else
            moteur.StartInfo.WorkingDirectory = Application.StartupPath
        End If
        If InStr(cheminMoteur, "mpiexec.exe", CompareMethod.Text) > 0 Then
            moteur.StartInfo.FileName = "mpiexec.exe"
            moteur.StartInfo.Arguments = cheminMoteur.Substring(InStr(cheminMoteur, "mpiexec.exe", CompareMethod.Text) + Len("mpiexec.exe"))
        Else
            moteur.StartInfo.FileName = ("""" & cheminMoteur & """")
        End If
        moteur.Start()
        moteur.PriorityClass = ProcessPriorityClass.Idle

        entreeMoteur = moteur.StandardInput
        erreurMoteur = moteur.StandardError

        entreeMoteur.WriteLine("uci")
        moteur.BeginOutputReadLine()

        While InStr(sortie, "uciok", CompareMethod.Text) = 0
            Application.DoEvents()
        End While
        sortie = ""

        'options communes
        If InStr(cheminMoteur, "rybka", CompareMethod.Text) > 0 Then
            entreeMoteur.WriteLine("setoption name Max CPUs value " & puissance)
        Else
            entreeMoteur.WriteLine("setoption name Threads value " & puissance)
        End If
        If modePonder = "1" Then
            entreeMoteur.WriteLine("setoption name Ponder value true")
        ElseIf modePonder = "0" Then
            entreeMoteur.WriteLine("setoption name Ponder value false")
        End If
        entreeMoteur.WriteLine("setoption name MultiPV value 1")
        entreeMoteur.WriteLine("setoption name Hash value " & memoire)
        entreeMoteur.WriteLine("setoption name Contempt value 0")

        'options spécifiques
        If InStr(cheminMoteur, "mpiexec.exe", CompareMethod.Text) > 0 Then
            optionsMoteur = Replace(cheminMoteur.Substring(cheminMoteur.LastIndexOf(" ") + 1), ".exe", ".txt")
        Else
            optionsMoteur = Replace(cheminMoteur.Substring(cheminMoteur.LastIndexOf("\") + 1), ".exe", ".txt")
        End If
        If My.Computer.FileSystem.FileExists(optionsMoteur) Then
            chaine = My.Computer.FileSystem.ReadAllText(optionsMoteur)
            tabChaine = Split(chaine, vbCrLf)
            For i = 0 To UBound(tabChaine)
                If tabChaine(i) <> "" Then
                    entreeMoteur.WriteLine(tabChaine(i))
                End If
                Application.DoEvents()
            Next
        End If

        'synchronisation
        entreeMoteur.WriteLine("isready")
        While InStr(sortie, "readyok", CompareMethod.Text) = 0
            Application.DoEvents()
        End While
        sortie = ""

        entreeMoteur.WriteLine("ucinewgame")
        entreeMoteur.WriteLine("isready")

        While InStr(sortie, "readyok", CompareMethod.Text) = 0
            Application.DoEvents()
        End While
        sortie = ""

        coeurs = cpu()
        'reconfigurer affinités :
        'si même machine avec 2 x sugar et ponder activé (puissance demandée inférieure ou égale à la moitié des coeurs)
        'pas besoin : si ponder désactivé ou si machine différente ou si autre moteur que sugar
        If modePonder = "1" And InStr(cheminMoteur, "sugar", CompareMethod.Text) > 0 And puissance <= CInt(coeurs / 2) Then
            'quand sugar est le moteur2 comme son affinité est en lecture seule
            'et qu'il utilise les threads dans l'ordre, il faut reconfigurer le moteur1
            'pour qu'il utilise d'autres threads
            affinite1 = binLong(StrDup(CInt(coeurs / 2), "0") & StrDup(CInt(coeurs / 2), "1"))
            affinite2 = binLong(StrDup(CInt(coeurs / 2), "1") & StrDup(CInt(coeurs / 2), "0"))
            '01-20 =       1048575 = 00000FFFFF
            '21-40 = 1099510579200 = FFFFF00000 = dépassement
            If modeReseau = "serveur" Then
                moteur.ProcessorAffinity = New IntPtr(affinite1)
            ElseIf modeReseau = "client" Then
                moteur.ProcessorAffinity = New IntPtr(affinite2)
            End If
        End If

    End Sub

    Public Function coupsEN(coups As String) As String
        Dim chaine As String

        chaine = Replace(coups, "F", "B")
        chaine = Replace(chaine, "C", "N")
        chaine = Replace(chaine, "D", "Q")
        chaine = Replace(chaine, "R", "K")
        chaine = Replace(chaine, "T", "R")

        Return chaine
    End Function

    Public Function coupsFR(coups As String) As String
        Dim chaine As String

        chaine = Replace(coups, "B", "F")
        chaine = Replace(chaine, "N", "C")
        chaine = Replace(chaine, "Q", "D")
        chaine = Replace(chaine, "R", "T")
        chaine = Replace(chaine, "K", "R")

        Return chaine
    End Function

    Public Function cpu(Optional reel As Boolean = False) As Integer
        Dim collection As New ManagementObjectSearcher("select * from Win32_Processor"), taches As Integer
        taches = 0

        For Each element As ManagementObject In collection.Get
            If reel Then
                taches = taches + element.Properties("NumberOfCores").Value 'cores
            Else
                taches = taches + element.Properties("NumberOfLogicalProcessors").Value 'threads
            End If
        Next

        Return taches
    End Function

    Public Function droite(texte As String, longueur As Integer) As String
        If longueur > 0 Then
            Return VB.Right(texte, longueur)
        Else
            Return ""
        End If
    End Function

    Public Sub echiquier(epd As String, e As PaintEventArgs, Optional coup1 As String = "", Optional coup2 As String = "")
        Dim tabEPD() As String, tabCaracteres() As Char
        Dim i As Integer, j As Integer, ligne As Integer, colonne As Integer
        Dim x As Integer, y As Integer, offsetGauche As Integer, offetHaut As Integer
        Dim largeur As Integer, hauteur As Integer, caseNoire As Boolean, chaine As String
        Dim trait As Char, stylo As System.Drawing.Pen, pinceau As System.Drawing.Brush
        Dim transparence As New ImageAttributes

        'minuscule = noirs
        'majuscule = blancs
        'w = trait blanc
        'b = trait noir
        '1-8 = cases vides

        'initialisation
        ligne = 1
        colonne = 1
        offsetGauche = -9
        offetHaut = -10
        largeur = 40 'grille
        hauteur = 40 'grille
        caseNoire = False 'en haut à gauche la case est blanche
        transparence.SetColorKey(Color.FromArgb(0, 255, 0), Color.FromArgb(0, 255, 0))

        'formatage
        For i = 2 To 8
            epd = Replace(epd, i, StrDup(i, "1"))
        Next

        'qui a le trait
        trait = ""
        If InStr(epd, " w ") > 0 Or InStr(epd, " b ") > 0 Then
            trait = epd.Chars(epd.IndexOf(" ") + 1)
        End If

        'dessiner la position de base
        tabEPD = Split(epd, "/")
        For i = 0 To UBound(tabEPD)
            If tabEPD(i) <> "" Then
                tabCaracteres = tabEPD(i).ToCharArray
                For j = 0 To UBound(tabCaracteres)
                    If tabCaracteres(j) = " " Then
                        Exit For
                    End If

                    x = colonne * largeur + offsetGauche  'centre case
                    y = ligne * hauteur + offetHaut 'centre case

                    'pièces
                    chaine = ""
                    Select Case tabCaracteres(j)
                        Case "r" 'rook = tour
                            chaine = "tourN"

                        Case "R"
                            chaine = "tourB"

                        Case "n" 'knight = cavalier
                            chaine = "cavalierN"

                        Case "N"
                            chaine = "cavalierB"

                        Case "b" 'bishop = fou
                            chaine = "fouN"

                        Case "B"
                            chaine = "fouB"

                        Case "q" 'queen = dame
                            chaine = "dameN"

                        Case "Q"
                            chaine = "dameB"

                        Case "k" 'king = roi
                            chaine = "roiN"

                        Case "K"
                            chaine = "roiB"

                        Case "p" 'pawn = pion
                            chaine = "pionN"

                        Case "P"
                            chaine = "pionB"

                    End Select

                    If chaine <> "" Then
                        e.Graphics.DrawImage(Image.FromFile("bmp\" & chaine & ".bmp"), New Rectangle(x - 14, y - 12, 37, 37), 0, 0, 37, 37, GraphicsUnit.Pixel, transparence)
                    End If

                    caseNoire = Not caseNoire
                    colonne = colonne + 1
                    If colonne = 9 Then
                        caseNoire = Not caseNoire
                        colonne = 1
                        ligne = ligne + 1
                    End If
                Next
            End If
        Next

        'coup moteur 1
        If coup1 <> "" And Not IsNothing(coup1) Then
            'moteurs d'accord ?
            If (Not IsNothing(coup2) And coup1 <> coup2) Or IsNothing(coup2) Then
                'non
                stylo = Pens.Blue
                pinceau = Brushes.Blue
            Else
                'oui
                stylo = Pens.Red
                pinceau = Brushes.Red
            End If

            echiquier_localisation(e, pinceau, stylo, coup1, tabEPD, trait, largeur, hauteur, offsetGauche)
        End If

        'coup moteur 2
        If coup2 <> "" And Not IsNothing(coup2) And ((Not IsNothing(coup1) And coup2 <> coup1) Or IsNothing(coup1)) Then
            stylo = Pens.Green
            pinceau = Brushes.Green

            echiquier_localisation(e, pinceau, stylo, coup2, tabEPD, trait, largeur, hauteur, offsetGauche)
        End If

        'trait
        If trait = "w" Then
            e.Graphics.FillEllipse(Brushes.White, New Rectangle(e.ClipRectangle.Width - 19, e.ClipRectangle.Height - 20, 15, 15))
            e.Graphics.DrawEllipse(Pens.Black, New Rectangle(e.ClipRectangle.Width - 19, e.ClipRectangle.Height - 20, 15, 15))
        ElseIf trait = "b" Then
            e.Graphics.FillEllipse(Brushes.Black, New Rectangle(e.ClipRectangle.Width - 19, 1, 15, 15))
            e.Graphics.DrawEllipse(Pens.Black, New Rectangle(e.ClipRectangle.Width - 19, 1, 15, 15))
        End If

        'totaux pièces
        e.Graphics.DrawString(epdTotalNoir(epd), New Font("courier new", 8), Brushes.Black, New Point(0, 0))
        e.Graphics.DrawString(epdTotalBlanc(epd), New Font("courier new", 8), Brushes.Black, New Point(0, e.ClipRectangle.Height - 16))

    End Sub

    Public Sub echiquier_localisation(e As PaintEventArgs, pinceau As System.Drawing.Brush, stylo As System.Drawing.Pen, coupMoteur As String, tabEPD() As String, trait As Char, largeur As Integer, hauteur As Integer, offsetGauche As Integer)
        Dim offsetRoque As Integer, x As Integer, y As Integer, colonne As Integer, ligne As Integer, xTrait As Integer, yTrait As Integer

        'rook
        offsetRoque = 7
        If coupMoteur = "e1g1" And tabEPD(7).Chars(4) = "K" And tabEPD(7).Chars(7) = "R" And trait = "w" Then
            '0-0
            'e1 => g1
            x = Val(Asc(coupMoteur.Chars(0)) - 96) * largeur + offsetGauche + 4
            y = (9 - Val(coupMoteur.Chars(1))) * hauteur - 4
            colonne = Val(Asc(coupMoteur.Chars(2)) - 96) * largeur + offsetGauche + 4
            ligne = (9 - Val(coupMoteur.Chars(3))) * hauteur - 4
            echiquier_dessin(e, pinceau, stylo, colonne - 4, ligne - 4 + offsetRoque, x + largeur / 2, y + offsetRoque, colonne, ligne + offsetRoque, x - largeur / 2, y - hauteur / 2, largeur, hauteur)

            'h1 => f1
            x = Val(Asc("h") - 96) * largeur + offsetGauche + 4
            y = (9 - 1) * hauteur - 4
            colonne = Val(Asc("f") - 96) * largeur + offsetGauche + 4
            ligne = (9 - 1) * hauteur - 4
            echiquier_dessin(e, pinceau, stylo, colonne - 4, ligne - 4 - offsetRoque, x - largeur / 2, y - offsetRoque, colonne, ligne - offsetRoque, x - largeur / 2, y - hauteur / 2, largeur, hauteur)

        ElseIf coupMoteur = "e1c1" And tabEPD(7).Chars(4) = "K" And tabEPD(7).Chars(0) = "R" And trait = "w" Then
            '0-0-0
            'e1 => c1
            x = Val(Asc(coupMoteur.Chars(0)) - 96) * largeur + offsetGauche + 4
            y = (9 - Val(coupMoteur.Chars(1))) * hauteur - 4
            colonne = Val(Asc(coupMoteur.Chars(2)) - 96) * largeur + offsetGauche + 4
            ligne = (9 - Val(coupMoteur.Chars(3))) * hauteur - 4
            echiquier_dessin(e, pinceau, stylo, colonne - 4, ligne - 4 + offsetRoque, x - largeur / 2, y + offsetRoque, colonne, ligne + offsetRoque, x - largeur / 2, y - hauteur / 2, largeur, hauteur)

            'a1 => d1
            x = Val(Asc("a") - 96) * largeur + offsetGauche + 4
            y = (9 - 1) * hauteur - 4
            colonne = Val(Asc("d") - 96) * largeur + offsetGauche + 4
            ligne = (9 - 1) * hauteur - 4
            echiquier_dessin(e, pinceau, stylo, colonne - 4, ligne - 4 - offsetRoque, x + largeur / 2, y - offsetRoque, colonne, ligne - offsetRoque, x - largeur / 2, y - hauteur / 2, largeur, hauteur)

        ElseIf coupMoteur = "e8g8" And tabEPD(0).Chars(4) = "k" And tabEPD(0).Chars(7) = "r" And trait = "b" Then
            '0-0
            'e8 => g8
            x = Val(Asc(coupMoteur.Chars(0)) - 96) * largeur + offsetGauche + 4
            y = (9 - Val(coupMoteur.Chars(1))) * hauteur - 4
            colonne = Val(Asc(coupMoteur.Chars(2)) - 96) * largeur + offsetGauche + 4
            ligne = (9 - Val(coupMoteur.Chars(3))) * hauteur - 4
            echiquier_dessin(e, pinceau, stylo, colonne - 4, ligne - 4 + offsetRoque, x + largeur / 2, y + offsetRoque, colonne, ligne + offsetRoque, x - largeur / 2, y - hauteur / 2, largeur, hauteur)

            'h8 => f8
            x = Val(Asc("h") - 96) * largeur + offsetGauche + 4
            y = (9 - 8) * hauteur - 4
            colonne = Val(Asc("f") - 96) * largeur + offsetGauche + 4
            ligne = (9 - 8) * hauteur - 4
            echiquier_dessin(e, pinceau, stylo, colonne - 4, ligne - 4 - offsetRoque, x - largeur / 2, y - offsetRoque, colonne, ligne - offsetRoque, x - largeur / 2, y - hauteur / 2, largeur, hauteur)

        ElseIf coupMoteur = "e8c8" And tabEPD(0).Chars(4) = "k" And tabEPD(0).Chars(0) = "r" And trait = "b" Then
            '0-0-0
            'e8 => c8
            x = Val(Asc(coupMoteur.Chars(0)) - 96) * largeur + offsetGauche + 4
            y = (9 - Val(coupMoteur.Chars(1))) * hauteur - 4
            colonne = Val(Asc(coupMoteur.Chars(2)) - 96) * largeur + offsetGauche + 4
            ligne = (9 - Val(coupMoteur.Chars(3))) * hauteur - 4
            echiquier_dessin(e, pinceau, stylo, colonne - 4, ligne - 4 + offsetRoque, x - largeur / 2, y + offsetRoque, colonne, ligne + offsetRoque, x - largeur / 2, y - hauteur / 2, largeur, hauteur)

            'a8 => d8
            x = Val(Asc("a") - 96) * largeur + offsetGauche + 4
            y = (9 - 8) * hauteur - 4
            colonne = Val(Asc("d") - 96) * largeur + offsetGauche + 4
            ligne = (9 - 8) * hauteur - 4
            echiquier_dessin(e, pinceau, stylo, colonne - 4, ligne - 4 - offsetRoque, x + largeur / 2, y - offsetRoque, colonne, ligne - offsetRoque, x - largeur / 2, y - hauteur / 2, largeur, hauteur)

        Else
            'coup simple
            x = Val(Asc(coupMoteur.Chars(0)) - 96) * largeur + offsetGauche + 4
            y = (9 - Val(coupMoteur.Chars(1))) * hauteur - 4
            colonne = Val(Asc(coupMoteur.Chars(2)) - 96) * largeur + offsetGauche + 4
            ligne = (9 - Val(coupMoteur.Chars(3))) * hauteur - 4
            xTrait = 0
            yTrait = 0
            offsetRoque = 0

            'traits
            If x = colonne Then
                'même colonne
                If y > ligne Then
                    'vers le haut
                    xTrait = x
                    yTrait = y - hauteur / 2
                Else
                    'vers le bas
                    xTrait = x
                    yTrait = y + hauteur / 2
                End If
            ElseIf y = ligne Then
                'même ligne
                If x < colonne Then
                    'vers la droite
                    xTrait = x + largeur / 2
                    yTrait = y
                Else
                    'vers la gauche
                    xTrait = x - largeur / 2
                    yTrait = y
                End If
            ElseIf colonne > x And ligne < y Then
                'vers le haut et vers la droite
                xTrait = x + largeur / 2
                yTrait = y - hauteur / 2
            ElseIf colonne < x And ligne < y Then
                'vers le haut et vers la gauche
                xTrait = x - largeur / 2
                yTrait = y - hauteur / 2
            ElseIf colonne > x And ligne > y Then
                'vers le bas et vers la droite
                xTrait = x + largeur / 2
                yTrait = y + hauteur / 2
            ElseIf colonne < x And ligne > y Then
                'vers le bas et vers la gauche
                xTrait = x - largeur / 2
                yTrait = y + hauteur / 2
            End If

            echiquier_dessin(e, pinceau, stylo, colonne - 4, ligne - 4, xTrait, yTrait, colonne, ligne, x - largeur / 2, y - hauteur / 2, largeur, hauteur)

        End If
    End Sub

    Public Sub echiquier_dessin(e As PaintEventArgs, pinceau As System.Drawing.Brush, stylo As System.Drawing.Pen, xRond As Integer, yRond As Integer, xTrait As Integer, yTrait As Integer, x1Trait As Integer, y1Trait As Integer, xContour As Integer, yContour As Integer, largContour As Integer, hautContour As Integer)
        'ronds
        e.Graphics.FillEllipse(pinceau, New Rectangle(xRond, yRond, 8, 8))

        'trait
        e.Graphics.DrawLine(stylo, New Point(xTrait, yTrait), New Point(x1Trait, y1Trait))

        'contours
        e.Graphics.DrawRectangle(stylo, New Rectangle(xContour, yContour, largContour, hautContour))
        e.Graphics.DrawRectangle(stylo, New Rectangle(xContour + 1, yContour + 1, largContour - 2, hautContour - 2))
        e.Graphics.DrawRectangle(stylo, New Rectangle(xContour + 2, yContour + 2, largContour - 4, hautContour - 4))

    End Sub

    Public Sub echiquier_differences(epd As String, e As PaintEventArgs)
        Dim i As Integer, piecesNoires As String, piecesBlanches As String, tmp() As Char
        Dim chaine As String, transparence As New ImageAttributes
        Dim pasN As Integer, pasB As Integer, larg As Integer

        larg = 20
        transparence.SetColorKey(Color.FromArgb(0, 255, 0), Color.FromArgb(0, 255, 0))

        'formatage
        For i = 1 To 8
            epd = Replace(epd, i, "")
        Next
        epd = Replace(epd, "/", "")
        If epd = "" Then
            Exit Sub
        End If
        epd = epd.Substring(0, epd.IndexOf(" "))

        'on liste les pièces noires
        piecesNoires = ""
        piecesBlanches = ""
        tmp = epd.ToCharArray
        For i = 0 To UBound(tmp)
            Select Case tmp(i)
                Case "r" 'rook = tour
                    piecesNoires = piecesNoires & tmp(i)

                Case "R"
                    piecesBlanches = piecesBlanches & tmp(i)

                Case "n" 'knight = cavalier
                    piecesNoires = piecesNoires & tmp(i)

                Case "N"
                    piecesBlanches = piecesBlanches & tmp(i)

                Case "b" 'bishop = fou
                    piecesNoires = piecesNoires & tmp(i)

                Case "B"
                    piecesBlanches = piecesBlanches & tmp(i)

                Case "q" 'queen = dame
                    piecesNoires = piecesNoires & tmp(i)

                Case "Q"
                    piecesBlanches = piecesBlanches & tmp(i)

                Case "p" 'pawn = pion
                    piecesNoires = piecesNoires & tmp(i)

                Case "P"
                    piecesBlanches = piecesBlanches & tmp(i)

            End Select
        Next

        'on supprime chaque pièce noir dans la liste des blanches
        tmp = piecesNoires.ToCharArray
        For i = 0 To UBound(tmp)
            If InStr(piecesBlanches, tmp(i), CompareMethod.Text) > 0 Then
                piecesBlanches = Replace(piecesBlanches, tmp(i), "", , 1, CompareMethod.Text)
                piecesNoires = Replace(piecesNoires, tmp(i), "", , 1)
            End If
        Next

        'on trie les pièces par valeur
        If piecesBlanches <> "" Then
            piecesBlanches = Replace(piecesBlanches, "Q", "1")
            piecesBlanches = Replace(piecesBlanches, "R", "2")
            piecesBlanches = Replace(piecesBlanches, "B", "3")
            piecesBlanches = Replace(piecesBlanches, "N", "4")
            piecesBlanches = Replace(piecesBlanches, "P", "5")
            'chaine vers tableau => tri alphabétique => tableau vers chaine
            tmp = piecesBlanches.ToCharArray
            Array.Sort(tmp)
            piecesBlanches = New String(tmp)
            'on met en forme la chaine
            piecesBlanches = Replace(piecesBlanches, "1", "Q")
            piecesBlanches = Replace(piecesBlanches, "2", "R")
            piecesBlanches = Replace(piecesBlanches, "3", "B")
            piecesBlanches = Replace(piecesBlanches, "4", "N")
            piecesBlanches = Replace(piecesBlanches, "5", "P")
        End If

        'on trie les pièces par valeur
        If piecesNoires <> "" Then
            piecesNoires = Replace(piecesNoires, "q", "1")
            piecesNoires = Replace(piecesNoires, "r", "2")
            piecesNoires = Replace(piecesNoires, "b", "3")
            piecesNoires = Replace(piecesNoires, "n", "4")
            piecesNoires = Replace(piecesNoires, "p", "5")
            'chaine vers tableau => tri alphabétique => tableau vers chaine
            tmp = piecesNoires.ToCharArray
            Array.Sort(tmp)
            piecesNoires = New String(tmp)
            'on met en forme la chaine
            piecesNoires = Replace(piecesNoires, "1", "q")
            piecesNoires = Replace(piecesNoires, "2", "r")
            piecesNoires = Replace(piecesNoires, "3", "b")
            piecesNoires = Replace(piecesNoires, "4", "n")
            piecesNoires = Replace(piecesNoires, "5", "p")
        End If

        'on cumule les pièces manquantes (noires à gauche, blanches à droite)
        chaine = piecesNoires & piecesBlanches

        If chaine = "" Then
            Exit Sub
        End If

        'pièces
        tmp = chaine.ToCharArray
        pasN = -1
        pasB = -1
        For i = 0 To UBound(tmp)
            Select Case tmp(i)
                Case "r" 'rook = tour
                    chaine = "tourN"

                Case "R"
                    chaine = "tourB"

                Case "n" 'knight = cavalier
                    chaine = "cavalierN"

                Case "N"
                    chaine = "cavalierB"

                Case "b" 'bishop = fou
                    chaine = "fouN"

                Case "B"
                    chaine = "fouB"

                Case "q" 'queen = dame
                    chaine = "dameN"

                Case "Q"
                    chaine = "dameB"

                Case "p" 'pawn = pion
                    chaine = "pionN"

                Case "P"
                    chaine = "pionB"

            End Select

            If chaine <> "" Then
                If InStr(chaine, "N") = Len(chaine) Then
                    pasN = pasN + 1
                    e.Graphics.DrawImage(Image.FromFile("bmp\" & chaine & ".bmp"), New Rectangle(15 + pasN * larg, 1, larg, larg), 0, 0, 37, 37, GraphicsUnit.Pixel, transparence)
                ElseIf InStr(chaine, "B") = Len(chaine) Then
                    pasB = pasB + 1
                    e.Graphics.DrawImage(Image.FromFile("bmp\" & chaine & ".bmp"), New Rectangle(e.ClipRectangle.Width / 2 + pasB * larg, 1, larg, larg), 0, 0, 37, 37, GraphicsUnit.Pixel, transparence)
                End If
            End If
        Next

    End Sub

    Public Function epdCasesOccupees(fen As String) As String
        Dim tabColonne() As String, casesOccupees As String, tabLignes() As String, chaine As String, caractere As String
        Dim ligne As Integer, index As Integer, coups As String

        tabColonne = {"", "a", "b", "c", "d", "e", "f", "g", "h"}
        casesOccupees = "a1;b1;c1;d1;e1;f1;g1;h1;a2;b2;c2;d2;e2;f2;g2;h2;a7;b7;c7;d7;e7;f7;g7;h7;a8;b8;c8;d8;e8;f8;g8;h8;"
        coups = ""

        'pour chaque ligne
        tabLignes = Split(gauche(fen, fen.IndexOf(" ")), "/")
        For ligne = UBound(tabLignes) To 0 Step -1
            'on remplace les chiffres par des "-" qui indique "case vide"
            chaine = ""
            For index = 0 To Len(tabLignes(ligne)) - 1
                caractere = tabLignes(ligne).Substring(index, 1)
                If IsNumeric(caractere) Then
                    chaine = chaine & StrDup(CInt(caractere), "-")
                Else
                    chaine = chaine & caractere
                End If
                Application.DoEvents()
            Next
            tabLignes(ligne) = chaine

            'on met à jour les cases occupées
            For index = 0 To Len(tabLignes(ligne)) - 1
                If tabLignes(ligne).Substring(index, 1) = "-" Then
                    'on efface
                    casesOccupees = Replace(casesOccupees, tabColonne(index + 1) & Format(tabLignes.Length - ligne) & ";", "")
                Else
                    'on ajoute
                    chaine = tabColonne(index + 1) & Format(tabLignes.Length - ligne)
                    caractere = tabLignes(ligne).Substring(index, 1)
                    If caractere = "p" Or caractere = "P" Then
                        caractere = ""
                    End If
                    coups = coups & UCase(caractere) & chaine & "-" & chaine & " "
                    If InStr(casesOccupees, chaine & ";") = 0 Then
                        casesOccupees = casesOccupees & chaine & ";"
                    End If
                End If
                Application.DoEvents()
            Next
            Application.DoEvents()
        Next

        casesOccupees = trierChaine(casesOccupees, ";") & ";"

        Return casesOccupees & ":" & coupsFR(coups)
    End Function

    Public Function epdTotalBlanc(fen As String) As Integer
        Dim tabCaracteres() As Char, i As Integer, total As Integer

        'initialisation
        total = 0

        tabCaracteres = fen.ToCharArray
        For i = 0 To UBound(tabCaracteres)
            If tabCaracteres(i) = " " Then
                Exit For
            End If

            'pièces
            Select Case tabCaracteres(i)
                Case "R"
                    total = total + 5

                Case "N"
                    total = total + 3

                Case "B"
                    total = total + 3

                Case "Q"
                    total = total + 9

                Case "P"
                    total = total + 1

            End Select
        Next

        Return total

    End Function

    Public Function epdTotalNoir(fen As String) As Integer
        Dim tabCaracteres() As Char, i As Integer, total As Integer

        'initialisation
        total = 0

        tabCaracteres = fen.ToCharArray
        For i = 0 To UBound(tabCaracteres)
            If tabCaracteres(i) = " " Then
                Exit For
            End If

            'pièces
            Select Case tabCaracteres(i)
                Case "r"
                    total = total + 5

                Case "n"
                    total = total + 3

                Case "b"
                    total = total + 3

                Case "q"
                    total = total + 9

                Case "p"
                    total = total + 1

            End Select
        Next

        Return total

    End Function

    Private Sub evenement(sendingProcess As Object, donnees As DataReceivedEventArgs)
        If InStr(sortie, donnees.Data) = 0 Then
            sortie = sortie & donnees.Data & vbCrLf
        End If
    End Sub

    Public Function formaterCoups(mode As String, coups As String, Optional index As Integer = 0) As String
        Dim chaine As String, i As Integer, tabChaine() As String

        chaine = Replace(coups, "+", "")
        chaine = Replace(chaine, " mate", "")

        Select Case mode
            Case "arena", "texte"
                'indexation
                If index > 0 Then
                    chaine = Format(Int(index / 2) + 1) & ". " & chaine
                End If

            Case "pgn"
                'on supprime les commentaires
                chaine = Replace(Replace(chaine, "??", ""), "!!", "")
                'indexation
                If index > 0 Then
                    chaine = Format(Int(index / 2) + 1) & ". " & chaine
                End If

            Case "moteur"
                'on intercepte les roques
                'coups blanc
                If (index Mod 2) = 1 Then
                    If InStr(chaine, "0-0-0", CompareMethod.Text) > 0 Then
                        chaine = "e1c1"
                    ElseIf InStr(chaine, "0-0", CompareMethod.Text) > 0 Then
                        chaine = "e1g1"
                    End If
                Else
                    'coups noir
                    If InStr(chaine, "0-0-0", CompareMethod.Text) > 0 Then
                        chaine = "e8c8"
                    ElseIf InStr(chaine, "0-0", CompareMethod.Text) > 0 Then
                        chaine = "e8g8"
                    End If
                End If
                'on supprime les commentaires
                chaine = Replace(Replace(chaine, "??", ""), "!!", "")
                'on retient uniquement la postion de départ/arrivée
                chaine = Replace(Replace(chaine, "-", ""), "x", "")
                'on supprime la prise en passant
                chaine = Replace(chaine, "ep", "")
                'on supprime l'info du type de pièce
                If Len(chaine) = 5 And IsNumeric(droite(chaine, 1)) Then
                    chaine = droite(chaine, 4)
                ElseIf InStr(chaine, "=", CompareMethod.Text) > 0 Then
                    chaine = coupsEN(Replace(chaine, "=", "")) 'g7g8=C
                End If

            Case "doublon"
                'on supprime les commentaires
                chaine = Replace(Replace(chaine, "??", ""), "!!", "")
                'on supprime l'indexation
                tabChaine = Split(chaine, " ")
                chaine = ""
                For i = 0 To UBound(tabChaine)
                    If InStr(tabChaine(i), ".", CompareMethod.Text) = 0 Then
                        chaine = chaine & tabChaine(i) & " "
                    End If
                Next
                chaine = Trim(chaine)

            Case Else
                'on supprime que les commentaires
                chaine = Replace(Replace(chaine, "??", ""), "!!", "")

        End Select

        Return chaine
    End Function

    Public Function gauche(texte As String, longueur As Integer) As String
        If longueur > 0 Then
            Return VB.Left(texte, longueur)
        Else
            Return ""
        End If
    End Function

    Public Function moteurEPD(moteur As String, moves As String, Optional startpos As String = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1") As String
        Dim processusEPD As New System.Diagnostics.Process(), entreeEPD As System.IO.StreamWriter, sortieEPD As System.IO.StreamReader, chaineEPD As String

        chaineEPD = startpos
        If moves <> "" Then
            'on charge le moteur
            processusEPD.StartInfo.RedirectStandardOutput = True
            processusEPD.StartInfo.UseShellExecute = False
            processusEPD.StartInfo.RedirectStandardInput = True
            processusEPD.StartInfo.RedirectStandardError = True
            processusEPD.StartInfo.CreateNoWindow = True
            processusEPD.StartInfo.FileName = moteur
            processusEPD.Start()

            entreeEPD = processusEPD.StandardInput
            sortieEPD = processusEPD.StandardOutput

            entreeEPD.WriteLine("position fen " & startpos & " moves " & moves)

            entreeEPD.WriteLine("d")

            chaineEPD = ""
            While InStr(chaineEPD, "Fen: ", CompareMethod.Text) = 0
                chaineEPD = sortieEPD.ReadLine
                Application.DoEvents()
            End While
            entreeEPD.WriteLine("quit")

            entreeEPD.Close()
            sortieEPD.Close()
            processusEPD.Close()

        End If

        entreeEPD = Nothing
        sortieEPD = Nothing
        processusEPD = Nothing

        Return Replace(chaineEPD, "Fen: ", "")
    End Function

    Public Sub positionnerMoteur(coups As String, fen As String)
        If fen = "" Then
            If coups = "" Then
                entreeMoteur.WriteLine("position startpos")
            ElseIf coups <> "" Then
                entreeMoteur.WriteLine("position startpos moves " & coups)
            End If
        Else
            If coups = "" Then
                entreeMoteur.WriteLine("position fen " & fen)
            ElseIf coups <> "" Then
                entreeMoteur.WriteLine("position fen " & fen & " moves " & coups)
            End If
        End If
    End Sub

    Public Function reception(socket As Socket) As String
        Dim message As String

        Try
            message = ""
            If socket.Available > 0 Then
                While InStr(message, finMessage, CompareMethod.Text) = 0
                    While socket.Available > 0
                        Dim tmp(socket.Available - 1) As Byte
                        socket.Receive(tmp)
                        message = message & System.Text.Encoding.ASCII.GetString(tmp)
                        Application.DoEvents()
                    End While
                    Application.DoEvents()
                End While

                If message <> "" Then
                    message = Replace(message, finMessage, "")
                    My.Computer.FileSystem.WriteAllText(My.Computer.Name & "_reception.log", Format(Now, "dd/MM/yyyy HH:mm:ss") & " " & message & vbCrLf, True)
                End If
            End If

            Return message

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "reception")
            End
        End Try
    End Function

    Public Function secJHMS(ByVal secondes As Long) As String
        Dim restant As Integer, valeur As Integer, chaine As String

        restant = secondes

        valeur = Fix(restant / 60 / 60 / 24) 'jours
        chaine = valeur
        restant = restant - valeur * 60 * 60 * 24

        valeur = Fix(restant / 60 / 60) 'heures
        chaine = chaine & ";" & valeur
        restant = restant - valeur * 60 * 60

        valeur = Fix(restant / 60) 'minutes
        chaine = chaine & ";" & valeur
        restant = restant - valeur * 60

        valeur = Fix(restant) 'secondes
        chaine = chaine & ";" & valeur

        'ex : secondes = 34849 => secJHMS = "0;9;40;49" ("Jours;Heures;Minutes;Secondes")

        Return chaine
    End Function

    Public Function structurePGN(partie As String, Optional fen As String = "", Optional blanc As String = "?", Optional noir As String = "?", Optional site As String = "?", Optional round As String = "?") As String
        Dim chaine As String, tabChaine() As String

        chaine = "[Event ""Computer chess game""]" & vbCrLf _
               & "[Site """ & site & """]" & vbCrLf _
               & "[Date """ & Format(Now, "yyyy.MM.dd") & """]" & vbCrLf _
               & "[Round """ & round & """]" & vbCrLf _
               & "[White """ & blanc & """]" & vbCrLf _
               & "[Black """ & noir & """]" & vbCrLf

        If fen <> "" Then
            chaine = chaine & "[SetUp ""1""]" & vbCrLf _
                            & "[FEN """ & fen & """]" & vbCrLf
        End If

        If InStr(partie, "1/2-1/2", CompareMethod.Text) = 0 And InStr(partie, "#", CompareMethod.Text) = 0 _
         And InStr(partie, " 1-0", CompareMethod.Text) = 0 And InStr(partie, " 0-1", CompareMethod.Text) = 0 Then
            chaine = chaine & "[Result ""*""]" & vbCrLf & vbCrLf _
                            & Trim(partie) & " *" & vbCrLf & vbCrLf

        ElseIf InStr(partie, "1/2-1/2", CompareMethod.Text) > 0 Then
            chaine = chaine & "[Result ""1/2-1/2""]" & vbCrLf & vbCrLf _
                            & Trim(partie) & vbCrLf & vbCrLf

        ElseIf InStr(partie, " 1-0", CompareMethod.Text) > 0 Then
            chaine = chaine & "[Result ""1-0""]" & vbCrLf & vbCrLf _
                            & Trim(partie) & vbCrLf & vbCrLf

        ElseIf InStr(partie, " 0-1", CompareMethod.Text) > 0 Then
            chaine = chaine & "[Result ""0-1""]" & vbCrLf & vbCrLf _
                            & Trim(partie) & vbCrLf & vbCrLf

        ElseIf InStr(partie, "#", CompareMethod.Text) > 0 Then
            tabChaine = Split(Trim(partie), " ")
            If InStr(tabChaine(UBound(tabChaine) - 1), ".", CompareMethod.Text) = 0 Then
                chaine = chaine & "[Result ""0-1""]" & vbCrLf & vbCrLf _
                                & Trim(partie) & " 0-1" & vbCrLf & vbCrLf
            Else
                chaine = chaine & "[Result ""1-0""]" & vbCrLf & vbCrLf _
                                & Trim(partie) & " 1-0" & vbCrLf & vbCrLf
            End If
        End If

        Return chaine
    End Function

    Public Function transmission(message As String, socket As Socket) As Integer

        Try
            My.Computer.FileSystem.WriteAllText(My.Computer.Name & "_transmission.log", Format(Now, "dd/MM/yyyy HH:mm:ss") & " " & message & vbCrLf, True)

            Return socket.Send(System.Text.Encoding.ASCII.GetBytes(message & finMessage))

        Catch ex As Exception
            If ex.HResult <> -2147467261 And ex.HResult <> -2147467259 Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, "emission")
            End If
            End
        End Try
    End Function

    Public Function trierChaine(serie As String, separateur As String, Optional ordre As Boolean = True) As String
        Dim tabChaine() As String

        tabChaine = Split(serie, separateur)
        If tabChaine(UBound(tabChaine)) = "" Then
            ReDim Preserve tabChaine(UBound(tabChaine) - 1)
        End If

        Array.Sort(tabChaine)
        If Not ordre Then
            Array.Reverse(tabChaine)
        End If

        Return String.Join(separateur, tabChaine)
    End Function

End Module
