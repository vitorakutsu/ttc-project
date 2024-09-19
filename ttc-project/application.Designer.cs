namespace ttc_project
{
    partial class Application
    {
        private PictureBox baseImagePictureBox;
        private PictureBox convertedImagePictureBox;
        private Button thinningButton_withoutDMA;
        private Button thinningButton_withDMA;
        private Button outlineExtractionButton_withoutDMA;
        private Button outlineExtractionButton_withDMA;
        private Button minimunRectangleButton_withoutDMA;
        private Button minimunRectangleButton_withDMA;
        private Button openImageButton;
        private Button clearImageButton;
        private OpenFileDialog openFileDialog;

        private System.ComponentModel.IContainer components = null;

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
            openFileDialog = new OpenFileDialog();
            baseImagePictureBox = new PictureBox();
            convertedImagePictureBox = new PictureBox();
            thinningButton_withoutDMA = new Button();
            thinningButton_withDMA = new Button();
            outlineExtractionButton_withoutDMA = new Button();
            outlineExtractionButton_withDMA = new Button();
            minimunRectangleButton_withoutDMA = new Button();
            minimunRectangleButton_withDMA = new Button();
            openImageButton = new Button();
            clearImageButton = new Button();
            ((System.ComponentModel.ISupportInitialize)baseImagePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)convertedImagePictureBox).BeginInit();
            SuspendLayout();
            // 
            // baseImagePictureBox
            // 
            baseImagePictureBox.BorderStyle = BorderStyle.FixedSingle;
            baseImagePictureBox.Location = new Point(12, 12);
            baseImagePictureBox.Name = "baseImagePictureBox";
            baseImagePictureBox.Size = new Size(797, 482);
            baseImagePictureBox.TabIndex = 0;
            baseImagePictureBox.TabStop = false;
            // 
            // convertedImagePictureBox
            // 
            convertedImagePictureBox.BorderStyle = BorderStyle.FixedSingle;
            convertedImagePictureBox.Location = new Point(815, 12);
            convertedImagePictureBox.Name = "convertedImagePictureBox";
            convertedImagePictureBox.Size = new Size(797, 482);
            convertedImagePictureBox.TabIndex = 1;
            convertedImagePictureBox.TabStop = false;
            // 
            // thinningButton_withoutDMA
            // 
            thinningButton_withoutDMA.Location = new Point(145, 531);
            thinningButton_withoutDMA.Name = "thinningButton_withoutDMA";
            thinningButton_withoutDMA.Size = new Size(156, 30);
            thinningButton_withoutDMA.TabIndex = 2;
            thinningButton_withoutDMA.Text = "Afinamento sem DMA";
            thinningButton_withoutDMA.Click += actionToThinningWithoutDMA;
            // 
            // thinningButton_withDMA
            // 
            thinningButton_withDMA.Location = new Point(145, 567);
            thinningButton_withDMA.Name = "thinningButton_withDMA";
            thinningButton_withDMA.Size = new Size(156, 30);
            thinningButton_withDMA.TabIndex = 3;
            thinningButton_withDMA.Text = "Afinamento com DMA";
            thinningButton_withDMA.Click += actionToThinningWithDMA;
            // 
            // outlineExtractionButton_withoutDMA
            // 
            outlineExtractionButton_withoutDMA.Location = new Point(307, 531);
            outlineExtractionButton_withoutDMA.Name = "outlineExtractionButton_withoutDMA";
            outlineExtractionButton_withoutDMA.Size = new Size(215, 30);
            outlineExtractionButton_withoutDMA.TabIndex = 4;
            outlineExtractionButton_withoutDMA.Text = "Extração de Contornos sem DMA";
            outlineExtractionButton_withoutDMA.Click += actionToOutlineExtractionWithoutDMA;
            // 
            // outlineExtractionButton_withDMA
            // 
            outlineExtractionButton_withDMA.Location = new Point(307, 567);
            outlineExtractionButton_withDMA.Name = "outlineExtractionButton_withDMA";
            outlineExtractionButton_withDMA.Size = new Size(215, 30);
            outlineExtractionButton_withDMA.TabIndex = 5;
            outlineExtractionButton_withDMA.Text = "Extração de Contornos com DMA";
            // 
            // minimunRectangleButton_withoutDMA
            // 
            minimunRectangleButton_withoutDMA.Location = new Point(528, 531);
            minimunRectangleButton_withoutDMA.Name = "minimunRectangleButton_withoutDMA";
            minimunRectangleButton_withoutDMA.Size = new Size(207, 30);
            minimunRectangleButton_withoutDMA.TabIndex = 6;
            minimunRectangleButton_withoutDMA.Text = "Retângulo Mínimo sem DMA";
            // 
            // minimunRectangleButton_withDMA
            // 
            minimunRectangleButton_withDMA.Location = new Point(528, 567);
            minimunRectangleButton_withDMA.Name = "minimunRectangleButton_withDMA";
            minimunRectangleButton_withDMA.Size = new Size(207, 30);
            minimunRectangleButton_withDMA.TabIndex = 7;
            minimunRectangleButton_withDMA.Text = "Retângulo Mínimo com DMA";
            // 
            // openImageButton
            // 
            openImageButton.Location = new Point(12, 531);
            openImageButton.Name = "openImageButton";
            openImageButton.Size = new Size(127, 30);
            openImageButton.TabIndex = 8;
            openImageButton.Text = "Abrir Imagem";
            openImageButton.Click += actionToOpenImage;
            // 
            // clearImageButton
            // 
            clearImageButton.Location = new Point(12, 567);
            clearImageButton.Name = "clearImageButton";
            clearImageButton.Size = new Size(127, 30);
            clearImageButton.TabIndex = 9;
            clearImageButton.Text = "Limpar Imagem";
            clearImageButton.Click += actionToClearImage;
            // 
            // Application
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1622, 641);
            Controls.Add(openImageButton);
            Controls.Add(clearImageButton);
            Controls.Add(baseImagePictureBox);
            Controls.Add(convertedImagePictureBox);
            Controls.Add(thinningButton_withoutDMA);
            Controls.Add(thinningButton_withDMA);
            Controls.Add(outlineExtractionButton_withoutDMA);
            Controls.Add(outlineExtractionButton_withDMA);
            Controls.Add(minimunRectangleButton_withoutDMA);
            Controls.Add(minimunRectangleButton_withDMA);
            Name = "Application";
            Text = "Application";
            ((System.ComponentModel.ISupportInitialize)baseImagePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)convertedImagePictureBox).EndInit();
            ResumeLayout(false);
        }
    }
}
