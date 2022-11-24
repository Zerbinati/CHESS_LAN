<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrincipale
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.rbServer = New System.Windows.Forms.RadioButton()
        Me.rbClient = New System.Windows.Forms.RadioButton()
        Me.cmdConnexion = New System.Windows.Forms.Button()
        Me.timerCommunication = New System.Windows.Forms.Timer(Me.components)
        Me.lstMessage = New System.Windows.Forms.ListBox()
        Me.ofdLAN = New System.Windows.Forms.OpenFileDialog()
        Me.lblDepthServer = New System.Windows.Forms.Label()
        Me.lblDepthClient = New System.Windows.Forms.Label()
        Me.lblSpeedClient = New System.Windows.Forms.Label()
        Me.lblSpeedServer = New System.Windows.Forms.Label()
        Me.lblPredictionClient = New System.Windows.Forms.Label()
        Me.lblPredictionServer = New System.Windows.Forms.Label()
        Me.txtHeure = New System.Windows.Forms.TextBox()
        Me.txtAction = New System.Windows.Forms.TextBox()
        Me.txtBestmove = New System.Windows.Forms.TextBox()
        Me.txtScore = New System.Windows.Forms.TextBox()
        Me.txtDepth = New System.Windows.Forms.TextBox()
        Me.txtEcoule = New System.Windows.Forms.TextBox()
        Me.txtSpeed = New System.Windows.Forms.TextBox()
        Me.txtMoveEngine = New System.Windows.Forms.TextBox()
        Me.txtCoupPonder = New System.Windows.Forms.TextBox()
        Me.lblHour = New System.Windows.Forms.Label()
        Me.lblAction = New System.Windows.Forms.Label()
        Me.lblBestmove = New System.Windows.Forms.Label()
        Me.lblMoveEngine = New System.Windows.Forms.Label()
        Me.lblCoupPonder = New System.Windows.Forms.Label()
        Me.lblScore = New System.Windows.Forms.Label()
        Me.lblDepth = New System.Windows.Forms.Label()
        Me.lblEcoule = New System.Windows.Forms.Label()
        Me.lblSpeed = New System.Windows.Forms.Label()
        Me.txtPositionFR = New System.Windows.Forms.TextBox()
        Me.pbScore = New System.Windows.Forms.PictureBox()
        Me.pbEchiquier = New System.Windows.Forms.PictureBox()
        Me.pbMateriel = New System.Windows.Forms.PictureBox()
        Me.cdrTourney = New System.Windows.Forms.GroupBox()
        Me.lblMaxMoves = New System.Windows.Forms.Label()
        Me.lblEndGame = New System.Windows.Forms.Label()
        Me.lblVictory = New System.Windows.Forms.Label()
        Me.lblFiftyMoves = New System.Windows.Forms.Label()
        Me.lblConsecutive = New System.Windows.Forms.Label()
        Me.lblClock = New System.Windows.Forms.Label()
        Me.lblEcouleCoup = New System.Windows.Forms.Label()
        Me.lblGameDuration = New System.Windows.Forms.Label()
        Me.lblGameMoves = New System.Windows.Forms.Label()
        Me.lblStats = New System.Windows.Forms.Label()
        Me.lblScores = New System.Windows.Forms.Label()
        Me.lblClient = New System.Windows.Forms.Label()
        Me.lblServer = New System.Windows.Forms.Label()
        Me.lblBlack = New System.Windows.Forms.Label()
        Me.lblWhite = New System.Windows.Forms.Label()
        Me.lblDrawGames = New System.Windows.Forms.Label()
        Me.lblClientScore = New System.Windows.Forms.Label()
        Me.lblServerScore = New System.Windows.Forms.Label()
        Me.lblBlackScore = New System.Windows.Forms.Label()
        Me.lblWhiteScore = New System.Windows.Forms.Label()
        Me.lblTraitNoir = New System.Windows.Forms.Label()
        Me.lblTraitBlanc = New System.Windows.Forms.Label()
        Me.lblBlackClock = New System.Windows.Forms.Label()
        Me.lblWhiteClock = New System.Windows.Forms.Label()
        Me.cdrSettings = New System.Windows.Forms.GroupBox()
        Me.cbPonder = New System.Windows.Forms.CheckBox()
        Me.lblIncrement = New System.Windows.Forms.Label()
        Me.lblMaxThreads = New System.Windows.Forms.Label()
        Me.cmdEngine = New System.Windows.Forms.Button()
        Me.txtEngine = New System.Windows.Forms.TextBox()
        Me.lblEngine = New System.Windows.Forms.Label()
        Me.cmdFEN = New System.Windows.Forms.Button()
        Me.txtFEN = New System.Windows.Forms.TextBox()
        Me.lblFEN = New System.Windows.Forms.Label()
        Me.lblMB = New System.Windows.Forms.Label()
        Me.txtHash = New System.Windows.Forms.TextBox()
        Me.lblHash = New System.Windows.Forms.Label()
        Me.txtThreads = New System.Windows.Forms.TextBox()
        Me.lblThreads = New System.Windows.Forms.Label()
        Me.lblMsec = New System.Windows.Forms.Label()
        Me.txtIncrement = New System.Windows.Forms.TextBox()
        Me.lblSec = New System.Windows.Forms.Label()
        Me.txtDelay = New System.Windows.Forms.TextBox()
        Me.lblDelay = New System.Windows.Forms.Label()
        CType(Me.pbScore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbEchiquier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbMateriel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cdrTourney.SuspendLayout()
        Me.cdrSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'rbServer
        '
        Me.rbServer.AutoSize = True
        Me.rbServer.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbServer.Location = New System.Drawing.Point(139, 8)
        Me.rbServer.Name = "rbServer"
        Me.rbServer.Size = New System.Drawing.Size(73, 24)
        Me.rbServer.TabIndex = 0
        Me.rbServer.Text = "Server"
        Me.rbServer.UseVisualStyleBackColor = True
        '
        'rbClient
        '
        Me.rbClient.AutoSize = True
        Me.rbClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbClient.Location = New System.Drawing.Point(139, 31)
        Me.rbClient.Name = "rbClient"
        Me.rbClient.Size = New System.Drawing.Size(67, 24)
        Me.rbClient.TabIndex = 1
        Me.rbClient.Text = "Client"
        Me.rbClient.UseVisualStyleBackColor = True
        '
        'cmdConnexion
        '
        Me.cmdConnexion.Enabled = False
        Me.cmdConnexion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdConnexion.Location = New System.Drawing.Point(11, 9)
        Me.cmdConnexion.Name = "cmdConnexion"
        Me.cmdConnexion.Size = New System.Drawing.Size(122, 42)
        Me.cmdConnexion.TabIndex = 2
        Me.cmdConnexion.Text = "LISTEN"
        Me.cmdConnexion.UseVisualStyleBackColor = True
        '
        'timerCommunication
        '
        '
        'lstMessage
        '
        Me.lstMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstMessage.FormattingEnabled = True
        Me.lstMessage.ItemHeight = 20
        Me.lstMessage.Location = New System.Drawing.Point(897, 8)
        Me.lstMessage.Name = "lstMessage"
        Me.lstMessage.Size = New System.Drawing.Size(355, 164)
        Me.lstMessage.TabIndex = 3
        '
        'lblDepthServer
        '
        Me.lblDepthServer.AutoSize = True
        Me.lblDepthServer.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepthServer.Location = New System.Drawing.Point(361, 10)
        Me.lblDepthServer.Name = "lblDepthServer"
        Me.lblDepthServer.Size = New System.Drawing.Size(102, 20)
        Me.lblDepthServer.TabIndex = 25
        Me.lblDepthServer.Text = "D0 (max. D0)"
        Me.lblDepthServer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDepthClient
        '
        Me.lblDepthClient.AutoSize = True
        Me.lblDepthClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepthClient.Location = New System.Drawing.Point(361, 33)
        Me.lblDepthClient.Name = "lblDepthClient"
        Me.lblDepthClient.Size = New System.Drawing.Size(102, 20)
        Me.lblDepthClient.TabIndex = 26
        Me.lblDepthClient.Text = "D0 (max. D0)"
        Me.lblDepthClient.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSpeedClient
        '
        Me.lblSpeedClient.AutoSize = True
        Me.lblSpeedClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpeedClient.Location = New System.Drawing.Point(505, 33)
        Me.lblSpeedClient.Name = "lblSpeedClient"
        Me.lblSpeedClient.Size = New System.Drawing.Size(118, 20)
        Me.lblSpeedClient.TabIndex = 34
        Me.lblSpeedClient.Text = "0 Knps (max. 0)"
        Me.lblSpeedClient.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSpeedServer
        '
        Me.lblSpeedServer.AutoSize = True
        Me.lblSpeedServer.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpeedServer.Location = New System.Drawing.Point(505, 10)
        Me.lblSpeedServer.Name = "lblSpeedServer"
        Me.lblSpeedServer.Size = New System.Drawing.Size(118, 20)
        Me.lblSpeedServer.TabIndex = 33
        Me.lblSpeedServer.Text = "0 Knps (max. 0)"
        Me.lblSpeedServer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPredictionClient
        '
        Me.lblPredictionClient.AutoSize = True
        Me.lblPredictionClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPredictionClient.Location = New System.Drawing.Point(719, 33)
        Me.lblPredictionClient.Name = "lblPredictionClient"
        Me.lblPredictionClient.Size = New System.Drawing.Size(120, 20)
        Me.lblPredictionClient.TabIndex = 46
        Me.lblPredictionClient.Text = "Ponder @ 0.0%"
        Me.lblPredictionClient.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPredictionServer
        '
        Me.lblPredictionServer.AutoSize = True
        Me.lblPredictionServer.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPredictionServer.Location = New System.Drawing.Point(719, 10)
        Me.lblPredictionServer.Name = "lblPredictionServer"
        Me.lblPredictionServer.Size = New System.Drawing.Size(120, 20)
        Me.lblPredictionServer.TabIndex = 45
        Me.lblPredictionServer.Text = "Ponder @ 0.0%"
        Me.lblPredictionServer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtHeure
        '
        Me.txtHeure.BackColor = System.Drawing.SystemColors.Window
        Me.txtHeure.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeure.Location = New System.Drawing.Point(10, 383)
        Me.txtHeure.Name = "txtHeure"
        Me.txtHeure.ReadOnly = True
        Me.txtHeure.Size = New System.Drawing.Size(85, 26)
        Me.txtHeure.TabIndex = 49
        Me.txtHeure.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtAction
        '
        Me.txtAction.BackColor = System.Drawing.SystemColors.Window
        Me.txtAction.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAction.Location = New System.Drawing.Point(101, 383)
        Me.txtAction.Name = "txtAction"
        Me.txtAction.ReadOnly = True
        Me.txtAction.Size = New System.Drawing.Size(199, 26)
        Me.txtAction.TabIndex = 51
        Me.txtAction.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtBestmove
        '
        Me.txtBestmove.BackColor = System.Drawing.SystemColors.Window
        Me.txtBestmove.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBestmove.Location = New System.Drawing.Point(307, 383)
        Me.txtBestmove.Name = "txtBestmove"
        Me.txtBestmove.ReadOnly = True
        Me.txtBestmove.Size = New System.Drawing.Size(121, 26)
        Me.txtBestmove.TabIndex = 53
        Me.txtBestmove.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtScore
        '
        Me.txtScore.BackColor = System.Drawing.SystemColors.Window
        Me.txtScore.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScore.Location = New System.Drawing.Point(566, 383)
        Me.txtScore.Name = "txtScore"
        Me.txtScore.ReadOnly = True
        Me.txtScore.Size = New System.Drawing.Size(63, 26)
        Me.txtScore.TabIndex = 55
        Me.txtScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDepth
        '
        Me.txtDepth.BackColor = System.Drawing.SystemColors.Window
        Me.txtDepth.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepth.Location = New System.Drawing.Point(635, 383)
        Me.txtDepth.Name = "txtDepth"
        Me.txtDepth.ReadOnly = True
        Me.txtDepth.Size = New System.Drawing.Size(55, 26)
        Me.txtDepth.TabIndex = 57
        Me.txtDepth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtEcoule
        '
        Me.txtEcoule.BackColor = System.Drawing.SystemColors.Window
        Me.txtEcoule.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEcoule.Location = New System.Drawing.Point(696, 383)
        Me.txtEcoule.Name = "txtEcoule"
        Me.txtEcoule.ReadOnly = True
        Me.txtEcoule.Size = New System.Drawing.Size(85, 26)
        Me.txtEcoule.TabIndex = 59
        Me.txtEcoule.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSpeed
        '
        Me.txtSpeed.BackColor = System.Drawing.SystemColors.Window
        Me.txtSpeed.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSpeed.Location = New System.Drawing.Point(787, 383)
        Me.txtSpeed.Name = "txtSpeed"
        Me.txtSpeed.ReadOnly = True
        Me.txtSpeed.Size = New System.Drawing.Size(104, 26)
        Me.txtSpeed.TabIndex = 61
        Me.txtSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMoveEngine
        '
        Me.txtMoveEngine.BackColor = System.Drawing.SystemColors.Window
        Me.txtMoveEngine.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMoveEngine.Location = New System.Drawing.Point(434, 383)
        Me.txtMoveEngine.Name = "txtMoveEngine"
        Me.txtMoveEngine.ReadOnly = True
        Me.txtMoveEngine.Size = New System.Drawing.Size(60, 26)
        Me.txtMoveEngine.TabIndex = 63
        Me.txtMoveEngine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtCoupPonder
        '
        Me.txtCoupPonder.BackColor = System.Drawing.SystemColors.Window
        Me.txtCoupPonder.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCoupPonder.Location = New System.Drawing.Point(500, 383)
        Me.txtCoupPonder.Name = "txtCoupPonder"
        Me.txtCoupPonder.ReadOnly = True
        Me.txtCoupPonder.Size = New System.Drawing.Size(60, 26)
        Me.txtCoupPonder.TabIndex = 65
        Me.txtCoupPonder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblHour
        '
        Me.lblHour.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHour.Location = New System.Drawing.Point(12, 360)
        Me.lblHour.Name = "lblHour"
        Me.lblHour.Size = New System.Drawing.Size(83, 20)
        Me.lblHour.TabIndex = 66
        Me.lblHour.Text = "Hour"
        Me.lblHour.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAction
        '
        Me.lblAction.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAction.Location = New System.Drawing.Point(101, 360)
        Me.lblAction.Name = "lblAction"
        Me.lblAction.Size = New System.Drawing.Size(199, 20)
        Me.lblAction.TabIndex = 67
        Me.lblAction.Text = "Action"
        Me.lblAction.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblBestmove
        '
        Me.lblBestmove.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBestmove.Location = New System.Drawing.Point(306, 360)
        Me.lblBestmove.Name = "lblBestmove"
        Me.lblBestmove.Size = New System.Drawing.Size(122, 20)
        Me.lblBestmove.TabIndex = 68
        Me.lblBestmove.Text = "Bestmove"
        Me.lblBestmove.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblMoveEngine
        '
        Me.lblMoveEngine.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoveEngine.Location = New System.Drawing.Point(434, 360)
        Me.lblMoveEngine.Name = "lblMoveEngine"
        Me.lblMoveEngine.Size = New System.Drawing.Size(60, 20)
        Me.lblMoveEngine.TabIndex = 69
        Me.lblMoveEngine.Text = "Engine"
        Me.lblMoveEngine.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblCoupPonder
        '
        Me.lblCoupPonder.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCoupPonder.Location = New System.Drawing.Point(500, 360)
        Me.lblCoupPonder.Name = "lblCoupPonder"
        Me.lblCoupPonder.Size = New System.Drawing.Size(60, 20)
        Me.lblCoupPonder.TabIndex = 70
        Me.lblCoupPonder.Text = "Ponder"
        Me.lblCoupPonder.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblScore
        '
        Me.lblScore.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScore.Location = New System.Drawing.Point(566, 360)
        Me.lblScore.Name = "lblScore"
        Me.lblScore.Size = New System.Drawing.Size(63, 20)
        Me.lblScore.TabIndex = 71
        Me.lblScore.Text = "Score"
        Me.lblScore.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblDepth
        '
        Me.lblDepth.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepth.Location = New System.Drawing.Point(636, 360)
        Me.lblDepth.Name = "lblDepth"
        Me.lblDepth.Size = New System.Drawing.Size(54, 20)
        Me.lblDepth.TabIndex = 72
        Me.lblDepth.Text = "Depth"
        Me.lblDepth.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblEcoule
        '
        Me.lblEcoule.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEcoule.Location = New System.Drawing.Point(696, 360)
        Me.lblEcoule.Name = "lblEcoule"
        Me.lblEcoule.Size = New System.Drawing.Size(85, 20)
        Me.lblEcoule.TabIndex = 73
        Me.lblEcoule.Text = "Tps écoulé"
        Me.lblEcoule.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblSpeed
        '
        Me.lblSpeed.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpeed.Location = New System.Drawing.Point(787, 360)
        Me.lblSpeed.Name = "lblSpeed"
        Me.lblSpeed.Size = New System.Drawing.Size(104, 20)
        Me.lblSpeed.TabIndex = 74
        Me.lblSpeed.Text = "Speed"
        Me.lblSpeed.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPositionFR
        '
        Me.txtPositionFR.BackColor = System.Drawing.SystemColors.Window
        Me.txtPositionFR.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPositionFR.Location = New System.Drawing.Point(10, 415)
        Me.txtPositionFR.Name = "txtPositionFR"
        Me.txtPositionFR.ReadOnly = True
        Me.txtPositionFR.Size = New System.Drawing.Size(881, 26)
        Me.txtPositionFR.TabIndex = 76
        '
        'pbScore
        '
        Me.pbScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbScore.Location = New System.Drawing.Point(9, 447)
        Me.pbScore.Name = "pbScore"
        Me.pbScore.Size = New System.Drawing.Size(882, 133)
        Me.pbScore.TabIndex = 84
        Me.pbScore.TabStop = False
        '
        'pbEchiquier
        '
        Me.pbEchiquier.BackColor = System.Drawing.SystemColors.Control
        Me.pbEchiquier.Location = New System.Drawing.Point(897, 194)
        Me.pbEchiquier.Name = "pbEchiquier"
        Me.pbEchiquier.Size = New System.Drawing.Size(355, 355)
        Me.pbEchiquier.TabIndex = 85
        Me.pbEchiquier.TabStop = False
        '
        'pbMateriel
        '
        Me.pbMateriel.BackColor = System.Drawing.SystemColors.Control
        Me.pbMateriel.Location = New System.Drawing.Point(897, 555)
        Me.pbMateriel.Name = "pbMateriel"
        Me.pbMateriel.Size = New System.Drawing.Size(355, 25)
        Me.pbMateriel.TabIndex = 86
        Me.pbMateriel.TabStop = False
        '
        'cdrTourney
        '
        Me.cdrTourney.Controls.Add(Me.lblMaxMoves)
        Me.cdrTourney.Controls.Add(Me.lblEndGame)
        Me.cdrTourney.Controls.Add(Me.lblVictory)
        Me.cdrTourney.Controls.Add(Me.lblFiftyMoves)
        Me.cdrTourney.Controls.Add(Me.lblConsecutive)
        Me.cdrTourney.Controls.Add(Me.lblClock)
        Me.cdrTourney.Controls.Add(Me.lblEcouleCoup)
        Me.cdrTourney.Controls.Add(Me.lblGameDuration)
        Me.cdrTourney.Controls.Add(Me.lblGameMoves)
        Me.cdrTourney.Controls.Add(Me.lblStats)
        Me.cdrTourney.Controls.Add(Me.lblScores)
        Me.cdrTourney.Controls.Add(Me.lblClient)
        Me.cdrTourney.Controls.Add(Me.lblServer)
        Me.cdrTourney.Controls.Add(Me.lblBlack)
        Me.cdrTourney.Controls.Add(Me.lblWhite)
        Me.cdrTourney.Controls.Add(Me.lblDrawGames)
        Me.cdrTourney.Controls.Add(Me.lblClientScore)
        Me.cdrTourney.Controls.Add(Me.lblServerScore)
        Me.cdrTourney.Controls.Add(Me.lblBlackScore)
        Me.cdrTourney.Controls.Add(Me.lblWhiteScore)
        Me.cdrTourney.Controls.Add(Me.lblTraitNoir)
        Me.cdrTourney.Controls.Add(Me.lblTraitBlanc)
        Me.cdrTourney.Controls.Add(Me.lblBlackClock)
        Me.cdrTourney.Controls.Add(Me.lblWhiteClock)
        Me.cdrTourney.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cdrTourney.Location = New System.Drawing.Point(11, 200)
        Me.cdrTourney.Name = "cdrTourney"
        Me.cdrTourney.Size = New System.Drawing.Size(880, 157)
        Me.cdrTourney.TabIndex = 87
        Me.cdrTourney.TabStop = False
        Me.cdrTourney.Text = "Tourney"
        '
        'lblMaxMoves
        '
        Me.lblMaxMoves.AutoSize = True
        Me.lblMaxMoves.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaxMoves.Location = New System.Drawing.Point(665, 100)
        Me.lblMaxMoves.Name = "lblMaxMoves"
        Me.lblMaxMoves.Size = New System.Drawing.Size(174, 20)
        Me.lblMaxMoves.TabIndex = 116
        Me.lblMaxMoves.Text = "200 moves before draw"
        '
        'lblEndGame
        '
        Me.lblEndGame.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEndGame.Location = New System.Drawing.Point(665, 22)
        Me.lblEndGame.Name = "lblEndGame"
        Me.lblEndGame.Size = New System.Drawing.Size(195, 27)
        Me.lblEndGame.TabIndex = 115
        Me.lblEndGame.Text = "END GAME"
        Me.lblEndGame.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblVictory
        '
        Me.lblVictory.AutoSize = True
        Me.lblVictory.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVictory.Location = New System.Drawing.Point(665, 125)
        Me.lblVictory.Name = "lblVictory"
        Me.lblVictory.Size = New System.Drawing.Size(145, 20)
        Me.lblVictory.TabIndex = 114
        Me.lblVictory.Text = "No imminent victory"
        '
        'lblFiftyMoves
        '
        Me.lblFiftyMoves.AutoSize = True
        Me.lblFiftyMoves.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFiftyMoves.Location = New System.Drawing.Point(665, 74)
        Me.lblFiftyMoves.Name = "lblFiftyMoves"
        Me.lblFiftyMoves.Size = New System.Drawing.Size(165, 20)
        Me.lblFiftyMoves.TabIndex = 113
        Me.lblFiftyMoves.Text = "50 moves before draw"
        '
        'lblConsecutive
        '
        Me.lblConsecutive.AutoSize = True
        Me.lblConsecutive.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsecutive.Location = New System.Drawing.Point(665, 49)
        Me.lblConsecutive.Name = "lblConsecutive"
        Me.lblConsecutive.Size = New System.Drawing.Size(182, 20)
        Me.lblConsecutive.TabIndex = 112
        Me.lblConsecutive.Text = "0 / 20 consecutive draws"
        '
        'lblClock
        '
        Me.lblClock.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClock.Location = New System.Drawing.Point(1, 22)
        Me.lblClock.Name = "lblClock"
        Me.lblClock.Size = New System.Drawing.Size(199, 22)
        Me.lblClock.TabIndex = 107
        Me.lblClock.Text = "CLOCK"
        Me.lblClock.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblEcouleCoup
        '
        Me.lblEcouleCoup.AutoSize = True
        Me.lblEcouleCoup.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEcouleCoup.Location = New System.Drawing.Point(467, 100)
        Me.lblEcouleCoup.Name = "lblEcouleCoup"
        Me.lblEcouleCoup.Size = New System.Drawing.Size(93, 20)
        Me.lblEcouleCoup.TabIndex = 105
        Me.lblEcouleCoup.Text = "0 ms / move"
        '
        'lblGameDuration
        '
        Me.lblGameDuration.AutoSize = True
        Me.lblGameDuration.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGameDuration.Location = New System.Drawing.Point(467, 74)
        Me.lblGameDuration.Name = "lblGameDuration"
        Me.lblGameDuration.Size = New System.Drawing.Size(99, 20)
        Me.lblGameDuration.TabIndex = 104
        Me.lblGameDuration.Text = "0 sec / game"
        '
        'lblGameMoves
        '
        Me.lblGameMoves.AutoSize = True
        Me.lblGameMoves.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGameMoves.Location = New System.Drawing.Point(467, 49)
        Me.lblGameMoves.Name = "lblGameMoves"
        Me.lblGameMoves.Size = New System.Drawing.Size(120, 20)
        Me.lblGameMoves.TabIndex = 103
        Me.lblGameMoves.Text = "0 moves / game"
        '
        'lblStats
        '
        Me.lblStats.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStats.Location = New System.Drawing.Point(467, 22)
        Me.lblStats.Name = "lblStats"
        Me.lblStats.Size = New System.Drawing.Size(145, 22)
        Me.lblStats.TabIndex = 102
        Me.lblStats.Text = "STATS"
        Me.lblStats.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblScores
        '
        Me.lblScores.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScores.Location = New System.Drawing.Point(229, 22)
        Me.lblScores.Name = "lblScores"
        Me.lblScores.Size = New System.Drawing.Size(223, 22)
        Me.lblScores.TabIndex = 100
        Me.lblScores.Text = "SCORES"
        Me.lblScores.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblClient
        '
        Me.lblClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClient.Location = New System.Drawing.Point(217, 125)
        Me.lblClient.Name = "lblClient"
        Me.lblClient.Size = New System.Drawing.Size(78, 20)
        Me.lblClient.TabIndex = 99
        Me.lblClient.Text = "Client :"
        '
        'lblServer
        '
        Me.lblServer.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServer.Location = New System.Drawing.Point(217, 100)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(78, 20)
        Me.lblServer.TabIndex = 98
        Me.lblServer.Text = "Server :"
        '
        'lblBlack
        '
        Me.lblBlack.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBlack.Location = New System.Drawing.Point(217, 74)
        Me.lblBlack.Name = "lblBlack"
        Me.lblBlack.Size = New System.Drawing.Size(78, 20)
        Me.lblBlack.TabIndex = 97
        Me.lblBlack.Text = "Black :"
        '
        'lblWhite
        '
        Me.lblWhite.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWhite.Location = New System.Drawing.Point(217, 49)
        Me.lblWhite.Name = "lblWhite"
        Me.lblWhite.Size = New System.Drawing.Size(78, 20)
        Me.lblWhite.TabIndex = 96
        Me.lblWhite.Text = "White :"
        '
        'lblDrawGames
        '
        Me.lblDrawGames.AutoSize = True
        Me.lblDrawGames.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrawGames.Location = New System.Drawing.Point(467, 125)
        Me.lblDrawGames.Name = "lblDrawGames"
        Me.lblDrawGames.Size = New System.Drawing.Size(140, 20)
        Me.lblDrawGames.TabIndex = 95
        Me.lblDrawGames.Text = "0% of draw games"
        '
        'lblClientScore
        '
        Me.lblClientScore.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClientScore.Location = New System.Drawing.Point(306, 125)
        Me.lblClientScore.Name = "lblClientScore"
        Me.lblClientScore.Size = New System.Drawing.Size(146, 20)
        Me.lblClientScore.TabIndex = 94
        Me.lblClientScore.Text = "0.0/0 (0%)"
        Me.lblClientScore.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblServerScore
        '
        Me.lblServerScore.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServerScore.Location = New System.Drawing.Point(306, 100)
        Me.lblServerScore.Name = "lblServerScore"
        Me.lblServerScore.Size = New System.Drawing.Size(146, 20)
        Me.lblServerScore.TabIndex = 93
        Me.lblServerScore.Text = "0.0/0 (0%)"
        Me.lblServerScore.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBlackScore
        '
        Me.lblBlackScore.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBlackScore.Location = New System.Drawing.Point(306, 74)
        Me.lblBlackScore.Name = "lblBlackScore"
        Me.lblBlackScore.Size = New System.Drawing.Size(146, 20)
        Me.lblBlackScore.TabIndex = 92
        Me.lblBlackScore.Text = "0.0/0 (0%)"
        Me.lblBlackScore.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWhiteScore
        '
        Me.lblWhiteScore.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWhiteScore.Location = New System.Drawing.Point(306, 49)
        Me.lblWhiteScore.Name = "lblWhiteScore"
        Me.lblWhiteScore.Size = New System.Drawing.Size(146, 20)
        Me.lblWhiteScore.TabIndex = 91
        Me.lblWhiteScore.Text = "0.0/0 (0%)"
        Me.lblWhiteScore.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTraitNoir
        '
        Me.lblTraitNoir.BackColor = System.Drawing.SystemColors.Control
        Me.lblTraitNoir.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTraitNoir.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblTraitNoir.Location = New System.Drawing.Point(107, 125)
        Me.lblTraitNoir.Name = "lblTraitNoir"
        Me.lblTraitNoir.Size = New System.Drawing.Size(93, 20)
        Me.lblTraitNoir.TabIndex = 89
        Me.lblTraitNoir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTraitBlanc
        '
        Me.lblTraitBlanc.BackColor = System.Drawing.SystemColors.Control
        Me.lblTraitBlanc.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTraitBlanc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTraitBlanc.Location = New System.Drawing.Point(8, 125)
        Me.lblTraitBlanc.Name = "lblTraitBlanc"
        Me.lblTraitBlanc.Size = New System.Drawing.Size(93, 20)
        Me.lblTraitBlanc.TabIndex = 88
        Me.lblTraitBlanc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBlackClock
        '
        Me.lblBlackClock.BackColor = System.Drawing.SystemColors.Control
        Me.lblBlackClock.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBlackClock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBlackClock.Location = New System.Drawing.Point(111, 70)
        Me.lblBlackClock.Name = "lblBlackClock"
        Me.lblBlackClock.Size = New System.Drawing.Size(89, 50)
        Me.lblBlackClock.TabIndex = 87
        Me.lblBlackClock.Text = "BLACK" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "00M00"
        Me.lblBlackClock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWhiteClock
        '
        Me.lblWhiteClock.BackColor = System.Drawing.SystemColors.Control
        Me.lblWhiteClock.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWhiteClock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWhiteClock.Location = New System.Drawing.Point(8, 70)
        Me.lblWhiteClock.Name = "lblWhiteClock"
        Me.lblWhiteClock.Size = New System.Drawing.Size(93, 50)
        Me.lblWhiteClock.TabIndex = 84
        Me.lblWhiteClock.Text = "WHITE" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "00M00"
        Me.lblWhiteClock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cdrSettings
        '
        Me.cdrSettings.Controls.Add(Me.cbPonder)
        Me.cdrSettings.Controls.Add(Me.lblIncrement)
        Me.cdrSettings.Controls.Add(Me.lblMaxThreads)
        Me.cdrSettings.Controls.Add(Me.cmdEngine)
        Me.cdrSettings.Controls.Add(Me.txtEngine)
        Me.cdrSettings.Controls.Add(Me.lblEngine)
        Me.cdrSettings.Controls.Add(Me.cmdFEN)
        Me.cdrSettings.Controls.Add(Me.txtFEN)
        Me.cdrSettings.Controls.Add(Me.lblFEN)
        Me.cdrSettings.Controls.Add(Me.lblMB)
        Me.cdrSettings.Controls.Add(Me.txtHash)
        Me.cdrSettings.Controls.Add(Me.lblHash)
        Me.cdrSettings.Controls.Add(Me.txtThreads)
        Me.cdrSettings.Controls.Add(Me.lblThreads)
        Me.cdrSettings.Controls.Add(Me.lblMsec)
        Me.cdrSettings.Controls.Add(Me.txtIncrement)
        Me.cdrSettings.Controls.Add(Me.lblSec)
        Me.cdrSettings.Controls.Add(Me.txtDelay)
        Me.cdrSettings.Controls.Add(Me.lblDelay)
        Me.cdrSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cdrSettings.Location = New System.Drawing.Point(10, 64)
        Me.cdrSettings.Name = "cdrSettings"
        Me.cdrSettings.Size = New System.Drawing.Size(881, 130)
        Me.cdrSettings.TabIndex = 88
        Me.cdrSettings.TabStop = False
        Me.cdrSettings.Text = "Settings"
        '
        'cbPonder
        '
        Me.cbPonder.AutoSize = True
        Me.cbPonder.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbPonder.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPonder.Location = New System.Drawing.Point(6, 26)
        Me.cbPonder.Name = "cbPonder"
        Me.cbPonder.Size = New System.Drawing.Size(79, 24)
        Me.cbPonder.TabIndex = 94
        Me.cbPonder.Text = "Ponder"
        Me.cbPonder.UseVisualStyleBackColor = True
        '
        'lblIncrement
        '
        Me.lblIncrement.AutoSize = True
        Me.lblIncrement.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIncrement.Location = New System.Drawing.Point(320, 27)
        Me.lblIncrement.Name = "lblIncrement"
        Me.lblIncrement.Size = New System.Drawing.Size(89, 20)
        Me.lblIncrement.TabIndex = 93
        Me.lblIncrement.Text = "Increment :"
        '
        'lblMaxThreads
        '
        Me.lblMaxThreads.AutoSize = True
        Me.lblMaxThreads.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaxThreads.Location = New System.Drawing.Point(666, 27)
        Me.lblMaxThreads.Name = "lblMaxThreads"
        Me.lblMaxThreads.Size = New System.Drawing.Size(22, 20)
        Me.lblMaxThreads.TabIndex = 92
        Me.lblMaxThreads.Text = "/0"
        '
        'cmdEngine
        '
        Me.cmdEngine.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEngine.Location = New System.Drawing.Point(840, 87)
        Me.cmdEngine.Name = "cmdEngine"
        Me.cmdEngine.Size = New System.Drawing.Size(35, 31)
        Me.cmdEngine.TabIndex = 91
        Me.cmdEngine.Text = "..."
        Me.cmdEngine.UseVisualStyleBackColor = True
        '
        'txtEngine
        '
        Me.txtEngine.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEngine.Location = New System.Drawing.Point(140, 89)
        Me.txtEngine.Name = "txtEngine"
        Me.txtEngine.Size = New System.Drawing.Size(690, 26)
        Me.txtEngine.TabIndex = 90
        '
        'lblEngine
        '
        Me.lblEngine.AutoSize = True
        Me.lblEngine.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEngine.Location = New System.Drawing.Point(6, 92)
        Me.lblEngine.Name = "lblEngine"
        Me.lblEngine.Size = New System.Drawing.Size(67, 20)
        Me.lblEngine.TabIndex = 89
        Me.lblEngine.Text = "Engine :"
        '
        'cmdFEN
        '
        Me.cmdFEN.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFEN.Location = New System.Drawing.Point(840, 54)
        Me.cmdFEN.Name = "cmdFEN"
        Me.cmdFEN.Size = New System.Drawing.Size(35, 31)
        Me.cmdFEN.TabIndex = 88
        Me.cmdFEN.Text = "..."
        Me.cmdFEN.UseVisualStyleBackColor = True
        '
        'txtFEN
        '
        Me.txtFEN.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFEN.Location = New System.Drawing.Point(140, 56)
        Me.txtFEN.Name = "txtFEN"
        Me.txtFEN.Size = New System.Drawing.Size(690, 26)
        Me.txtFEN.TabIndex = 87
        '
        'lblFEN
        '
        Me.lblFEN.AutoSize = True
        Me.lblFEN.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFEN.Location = New System.Drawing.Point(8, 59)
        Me.lblFEN.Name = "lblFEN"
        Me.lblFEN.Size = New System.Drawing.Size(49, 20)
        Me.lblFEN.TabIndex = 86
        Me.lblFEN.Text = "FEN :"
        '
        'lblMB
        '
        Me.lblMB.AutoSize = True
        Me.lblMB.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMB.Location = New System.Drawing.Point(844, 27)
        Me.lblMB.Name = "lblMB"
        Me.lblMB.Size = New System.Drawing.Size(33, 20)
        Me.lblMB.TabIndex = 85
        Me.lblMB.Text = "MB"
        '
        'txtHash
        '
        Me.txtHash.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHash.Location = New System.Drawing.Point(776, 24)
        Me.txtHash.Name = "txtHash"
        Me.txtHash.Size = New System.Drawing.Size(62, 26)
        Me.txtHash.TabIndex = 84
        Me.txtHash.Text = "1024"
        Me.txtHash.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblHash
        '
        Me.lblHash.AutoSize = True
        Me.lblHash.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHash.Location = New System.Drawing.Point(716, 27)
        Me.lblHash.Name = "lblHash"
        Me.lblHash.Size = New System.Drawing.Size(55, 20)
        Me.lblHash.TabIndex = 83
        Me.lblHash.Text = "Hash :"
        '
        'txtThreads
        '
        Me.txtThreads.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtThreads.Location = New System.Drawing.Point(625, 24)
        Me.txtThreads.Name = "txtThreads"
        Me.txtThreads.Size = New System.Drawing.Size(39, 26)
        Me.txtThreads.TabIndex = 82
        Me.txtThreads.Text = "1"
        Me.txtThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblThreads
        '
        Me.lblThreads.AutoSize = True
        Me.lblThreads.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblThreads.Location = New System.Drawing.Point(544, 27)
        Me.lblThreads.Name = "lblThreads"
        Me.lblThreads.Size = New System.Drawing.Size(75, 20)
        Me.lblThreads.TabIndex = 81
        Me.lblThreads.Text = "Threads :"
        '
        'lblMsec
        '
        Me.lblMsec.AutoSize = True
        Me.lblMsec.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMsec.Location = New System.Drawing.Point(477, 27)
        Me.lblMsec.Name = "lblMsec"
        Me.lblMsec.Size = New System.Drawing.Size(30, 20)
        Me.lblMsec.TabIndex = 80
        Me.lblMsec.Text = "ms"
        '
        'txtIncrement
        '
        Me.txtIncrement.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIncrement.Location = New System.Drawing.Point(415, 24)
        Me.txtIncrement.Name = "txtIncrement"
        Me.txtIncrement.Size = New System.Drawing.Size(56, 26)
        Me.txtIncrement.TabIndex = 79
        Me.txtIncrement.Text = "2000"
        Me.txtIncrement.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblSec
        '
        Me.lblSec.AutoSize = True
        Me.lblSec.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSec.Location = New System.Drawing.Point(262, 27)
        Me.lblSec.Name = "lblSec"
        Me.lblSec.Size = New System.Drawing.Size(34, 20)
        Me.lblSec.TabIndex = 78
        Me.lblSec.Text = "sec"
        '
        'txtDelay
        '
        Me.txtDelay.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDelay.Location = New System.Drawing.Point(202, 24)
        Me.txtDelay.Name = "txtDelay"
        Me.txtDelay.Size = New System.Drawing.Size(54, 26)
        Me.txtDelay.TabIndex = 77
        Me.txtDelay.Text = "180"
        Me.txtDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblDelay
        '
        Me.lblDelay.AutoSize = True
        Me.lblDelay.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDelay.Location = New System.Drawing.Point(130, 27)
        Me.lblDelay.Name = "lblDelay"
        Me.lblDelay.Size = New System.Drawing.Size(57, 20)
        Me.lblDelay.TabIndex = 76
        Me.lblDelay.Text = "Delay :"
        '
        'frmPrincipale
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1264, 591)
        Me.Controls.Add(Me.cdrSettings)
        Me.Controls.Add(Me.cdrTourney)
        Me.Controls.Add(Me.pbMateriel)
        Me.Controls.Add(Me.pbEchiquier)
        Me.Controls.Add(Me.pbScore)
        Me.Controls.Add(Me.txtPositionFR)
        Me.Controls.Add(Me.lblSpeed)
        Me.Controls.Add(Me.lblEcoule)
        Me.Controls.Add(Me.lblDepth)
        Me.Controls.Add(Me.lblScore)
        Me.Controls.Add(Me.lblCoupPonder)
        Me.Controls.Add(Me.lblMoveEngine)
        Me.Controls.Add(Me.lblBestmove)
        Me.Controls.Add(Me.lblAction)
        Me.Controls.Add(Me.lblHour)
        Me.Controls.Add(Me.txtCoupPonder)
        Me.Controls.Add(Me.txtMoveEngine)
        Me.Controls.Add(Me.txtSpeed)
        Me.Controls.Add(Me.txtEcoule)
        Me.Controls.Add(Me.txtDepth)
        Me.Controls.Add(Me.txtScore)
        Me.Controls.Add(Me.txtBestmove)
        Me.Controls.Add(Me.txtAction)
        Me.Controls.Add(Me.txtHeure)
        Me.Controls.Add(Me.lblPredictionClient)
        Me.Controls.Add(Me.lblPredictionServer)
        Me.Controls.Add(Me.lblSpeedClient)
        Me.Controls.Add(Me.lblSpeedServer)
        Me.Controls.Add(Me.lblDepthClient)
        Me.Controls.Add(Me.lblDepthServer)
        Me.Controls.Add(Me.lstMessage)
        Me.Controls.Add(Me.cmdConnexion)
        Me.Controls.Add(Me.rbClient)
        Me.Controls.Add(Me.rbServer)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmPrincipale"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CHESS LAN"
        CType(Me.pbScore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbEchiquier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbMateriel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cdrTourney.ResumeLayout(False)
        Me.cdrTourney.PerformLayout()
        Me.cdrSettings.ResumeLayout(False)
        Me.cdrSettings.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rbServer As System.Windows.Forms.RadioButton
    Friend WithEvents rbClient As System.Windows.Forms.RadioButton
    Friend WithEvents cmdConnexion As System.Windows.Forms.Button
    Friend WithEvents timerCommunication As System.Windows.Forms.Timer
    Friend WithEvents lstMessage As System.Windows.Forms.ListBox
    Friend WithEvents ofdLAN As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblDepthServer As System.Windows.Forms.Label
    Friend WithEvents lblDepthClient As System.Windows.Forms.Label
    Friend WithEvents lblSpeedClient As System.Windows.Forms.Label
    Friend WithEvents lblSpeedServer As System.Windows.Forms.Label
    Friend WithEvents lblPredictionClient As System.Windows.Forms.Label
    Friend WithEvents lblPredictionServer As System.Windows.Forms.Label
    Friend WithEvents txtHeure As System.Windows.Forms.TextBox
    Friend WithEvents txtAction As System.Windows.Forms.TextBox
    Friend WithEvents txtBestmove As System.Windows.Forms.TextBox
    Friend WithEvents txtScore As System.Windows.Forms.TextBox
    Friend WithEvents txtDepth As System.Windows.Forms.TextBox
    Friend WithEvents txtEcoule As System.Windows.Forms.TextBox
    Friend WithEvents txtSpeed As System.Windows.Forms.TextBox
    Friend WithEvents txtMoveEngine As System.Windows.Forms.TextBox
    Friend WithEvents txtCoupPonder As System.Windows.Forms.TextBox
    Friend WithEvents lblHour As System.Windows.Forms.Label
    Friend WithEvents lblAction As System.Windows.Forms.Label
    Friend WithEvents lblBestmove As System.Windows.Forms.Label
    Friend WithEvents lblMoveEngine As System.Windows.Forms.Label
    Friend WithEvents lblCoupPonder As System.Windows.Forms.Label
    Friend WithEvents lblScore As System.Windows.Forms.Label
    Friend WithEvents lblDepth As System.Windows.Forms.Label
    Friend WithEvents lblEcoule As System.Windows.Forms.Label
    Friend WithEvents lblSpeed As System.Windows.Forms.Label
    Friend WithEvents txtPositionFR As System.Windows.Forms.TextBox
    Friend WithEvents pbScore As System.Windows.Forms.PictureBox
    Friend WithEvents pbEchiquier As System.Windows.Forms.PictureBox
    Friend WithEvents pbMateriel As System.Windows.Forms.PictureBox
    Friend WithEvents cdrTourney As System.Windows.Forms.GroupBox
    Friend WithEvents lblEcouleCoup As System.Windows.Forms.Label
    Friend WithEvents lblGameDuration As System.Windows.Forms.Label
    Friend WithEvents lblGameMoves As System.Windows.Forms.Label
    Friend WithEvents lblStats As System.Windows.Forms.Label
    Friend WithEvents lblScores As System.Windows.Forms.Label
    Friend WithEvents lblClient As System.Windows.Forms.Label
    Friend WithEvents lblServer As System.Windows.Forms.Label
    Friend WithEvents lblBlack As System.Windows.Forms.Label
    Friend WithEvents lblWhite As System.Windows.Forms.Label
    Friend WithEvents lblDrawGames As System.Windows.Forms.Label
    Friend WithEvents lblClientScore As System.Windows.Forms.Label
    Friend WithEvents lblServerScore As System.Windows.Forms.Label
    Friend WithEvents lblBlackScore As System.Windows.Forms.Label
    Friend WithEvents lblWhiteScore As System.Windows.Forms.Label
    Friend WithEvents lblTraitNoir As System.Windows.Forms.Label
    Friend WithEvents lblTraitBlanc As System.Windows.Forms.Label
    Friend WithEvents lblBlackClock As System.Windows.Forms.Label
    Friend WithEvents lblWhiteClock As System.Windows.Forms.Label
    Friend WithEvents cdrSettings As System.Windows.Forms.GroupBox
    Friend WithEvents cbPonder As System.Windows.Forms.CheckBox
    Friend WithEvents lblIncrement As System.Windows.Forms.Label
    Friend WithEvents lblMaxThreads As System.Windows.Forms.Label
    Friend WithEvents cmdEngine As System.Windows.Forms.Button
    Friend WithEvents txtEngine As System.Windows.Forms.TextBox
    Friend WithEvents lblEngine As System.Windows.Forms.Label
    Friend WithEvents cmdFEN As System.Windows.Forms.Button
    Friend WithEvents txtFEN As System.Windows.Forms.TextBox
    Friend WithEvents lblFEN As System.Windows.Forms.Label
    Friend WithEvents lblMB As System.Windows.Forms.Label
    Friend WithEvents txtHash As System.Windows.Forms.TextBox
    Friend WithEvents lblHash As System.Windows.Forms.Label
    Friend WithEvents txtThreads As System.Windows.Forms.TextBox
    Friend WithEvents lblThreads As System.Windows.Forms.Label
    Friend WithEvents lblMsec As System.Windows.Forms.Label
    Friend WithEvents txtIncrement As System.Windows.Forms.TextBox
    Friend WithEvents lblSec As System.Windows.Forms.Label
    Friend WithEvents txtDelay As System.Windows.Forms.TextBox
    Friend WithEvents lblDelay As System.Windows.Forms.Label
    Friend WithEvents lblClock As System.Windows.Forms.Label
    Friend WithEvents lblMaxMoves As System.Windows.Forms.Label
    Friend WithEvents lblEndGame As System.Windows.Forms.Label
    Friend WithEvents lblVictory As System.Windows.Forms.Label
    Friend WithEvents lblFiftyMoves As System.Windows.Forms.Label
    Friend WithEvents lblConsecutive As System.Windows.Forms.Label

End Class
