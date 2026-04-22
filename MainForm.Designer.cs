namespace MinesweeperGame;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private Label titleLabel = null!;
    private Label subtitleLabel = null!;
    private Label playerLabel = null!;
    private TextBox playerNameTextBox = null!;
    private Label highScoreTextLabel = null!;
    private Label highScoreValueLabel = null!;
    private Label statusTextLabel = null!;
    private Label statusValueLabel = null!;
    private Label instructionsLabel = null!;
    private TableLayoutPanel boardTableLayoutPanel = null!;
    private Button resetButton = null!;
    private Button saveScoreButton = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        titleLabel = new Label();
        subtitleLabel = new Label();
        playerLabel = new Label();
        playerNameTextBox = new TextBox();
        highScoreTextLabel = new Label();
        highScoreValueLabel = new Label();
        statusTextLabel = new Label();
        statusValueLabel = new Label();
        instructionsLabel = new Label();
        boardTableLayoutPanel = new TableLayoutPanel();
        resetButton = new Button();
        saveScoreButton = new Button();
        SuspendLayout();
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
        titleLabel.Location = new Point(24, 18);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(180, 32);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Minesweeper";
        // 
        // subtitleLabel
        // 
        subtitleLabel.AutoSize = true;
        subtitleLabel.ForeColor = SystemColors.GrayText;
        subtitleLabel.Location = new Point(28, 50);
        subtitleLabel.Name = "subtitleLabel";
        subtitleLabel.Size = new Size(146, 15);
        subtitleLabel.TabIndex = 1;
        subtitleLabel.Text = "Clear every safe square.";
        // 
        // playerLabel
        // 
        playerLabel.AutoSize = true;
        playerLabel.Location = new Point(28, 81);
        playerLabel.Name = "playerLabel";
        playerLabel.Size = new Size(43, 15);
        playerLabel.TabIndex = 2;
        playerLabel.Text = "Player:";
        // 
        // playerNameTextBox
        // 
        playerNameTextBox.Location = new Point(77, 78);
        playerNameTextBox.Name = "playerNameTextBox";
        playerNameTextBox.Size = new Size(176, 23);
        playerNameTextBox.TabIndex = 3;
        playerNameTextBox.Leave += PlayerNameTextBox_Leave;
        // 
        // highScoreTextLabel
        // 
        highScoreTextLabel.AutoSize = true;
        highScoreTextLabel.Location = new Point(274, 81);
        highScoreTextLabel.Name = "highScoreTextLabel";
        highScoreTextLabel.Size = new Size(68, 15);
        highScoreTextLabel.TabIndex = 4;
        highScoreTextLabel.Text = "Best Score:";
        // 
        // highScoreValueLabel
        // 
        highScoreValueLabel.AutoSize = true;
        highScoreValueLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        highScoreValueLabel.Location = new Point(348, 81);
        highScoreValueLabel.Name = "highScoreValueLabel";
        highScoreValueLabel.Size = new Size(14, 15);
        highScoreValueLabel.TabIndex = 5;
        highScoreValueLabel.Text = "0";
        // 
        // statusTextLabel
        // 
        statusTextLabel.AutoSize = true;
        statusTextLabel.Location = new Point(28, 114);
        statusTextLabel.Name = "statusTextLabel";
        statusTextLabel.Size = new Size(42, 15);
        statusTextLabel.TabIndex = 6;
        statusTextLabel.Text = "Status:";
        // 
        // statusValueLabel
        // 
        statusValueLabel.Location = new Point(76, 114);
        statusValueLabel.Name = "statusValueLabel";
        statusValueLabel.Size = new Size(330, 15);
        statusValueLabel.TabIndex = 7;
        statusValueLabel.Text = "Reveal a square to begin.";
        // 
        // instructionsLabel
        // 
        instructionsLabel.BorderStyle = BorderStyle.FixedSingle;
        instructionsLabel.Location = new Point(28, 146);
        instructionsLabel.Name = "instructionsLabel";
        instructionsLabel.Padding = new Padding(8);
        instructionsLabel.Size = new Size(378, 56);
        instructionsLabel.TabIndex = 8;
        instructionsLabel.Text = "Left-click reveals a square. Right-click places or removes a flag. Save your score after a strong round.";
        // 
        // boardTableLayoutPanel
        // 
        boardTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        boardTableLayoutPanel.ColumnCount = 6;
        boardTableLayoutPanel.Location = new Point(28, 219);
        boardTableLayoutPanel.Name = "boardTableLayoutPanel";
        boardTableLayoutPanel.RowCount = 6;
        boardTableLayoutPanel.Size = new Size(360, 360);
        boardTableLayoutPanel.TabIndex = 9;
        // 
        // resetButton
        // 
        resetButton.Location = new Point(28, 598);
        resetButton.Name = "resetButton";
        resetButton.Size = new Size(112, 34);
        resetButton.TabIndex = 10;
        resetButton.Text = "New Game";
        resetButton.UseVisualStyleBackColor = true;
        resetButton.Click += ResetButton_Click;
        // 
        // saveScoreButton
        // 
        saveScoreButton.Location = new Point(157, 598);
        saveScoreButton.Name = "saveScoreButton";
        saveScoreButton.Size = new Size(130, 34);
        saveScoreButton.TabIndex = 11;
        saveScoreButton.Text = "Save Score";
        saveScoreButton.UseVisualStyleBackColor = true;
        saveScoreButton.Click += SaveScoreButton_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(434, 652);
        Controls.Add(saveScoreButton);
        Controls.Add(resetButton);
        Controls.Add(boardTableLayoutPanel);
        Controls.Add(instructionsLabel);
        Controls.Add(statusValueLabel);
        Controls.Add(statusTextLabel);
        Controls.Add(highScoreValueLabel);
        Controls.Add(highScoreTextLabel);
        Controls.Add(playerNameTextBox);
        Controls.Add(playerLabel);
        Controls.Add(subtitleLabel);
        Controls.Add(titleLabel);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Minesweeper Game";
        ResumeLayout(false);
        PerformLayout();
    }
}
