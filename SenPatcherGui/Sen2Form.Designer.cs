﻿namespace SenPatcherGui {
	partial class Sen2Form {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.checkBoxBattleAutoSkip = new System.Windows.Forms.CheckBox();
			this.labelVersion = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.labelFile = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonPatch = new System.Windows.Forms.Button();
			this.checkBoxPatchAudioThread = new System.Windows.Forms.CheckBox();
			this.checkBoxBgmEnqueueingLogic = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.buttonUnpatch = new System.Windows.Forms.Button();
			this.checkBoxAssetPatches = new System.Windows.Forms.CheckBox();
			this.buttonAssetFixDetails = new System.Windows.Forms.Button();
			this.checkBoxFixVoiceFileLang = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(94, 256);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(401, 26);
			this.label5.TabIndex = 21;
			this.label5.Text = "Makes the audio thread\'s timing more consistent, which fixes overly long BGM fade" +
    "s\r\nand some missing sound effects such as Fie\'s Sweep Craft.\r\n";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 111);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Patch Settings:";
			// 
			// checkBoxBattleAutoSkip
			// 
			this.checkBoxBattleAutoSkip.AutoSize = true;
			this.checkBoxBattleAutoSkip.Checked = true;
			this.checkBoxBattleAutoSkip.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxBattleAutoSkip.Location = new System.Drawing.Point(97, 134);
			this.checkBoxBattleAutoSkip.Name = "checkBoxBattleAutoSkip";
			this.checkBoxBattleAutoSkip.Size = new System.Drawing.Size(207, 17);
			this.checkBoxBattleAutoSkip.TabIndex = 15;
			this.checkBoxBattleAutoSkip.Text = "Remove animation skip in Turbo mode";
			this.checkBoxBattleAutoSkip.UseVisualStyleBackColor = true;
			// 
			// labelVersion
			// 
			this.labelVersion.AutoSize = true;
			this.labelVersion.Location = new System.Drawing.Point(57, 30);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(16, 13);
			this.labelVersion.TabIndex = 14;
			this.labelVersion.Text = "---";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 30);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(45, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "Version:";
			// 
			// labelFile
			// 
			this.labelFile.AutoSize = true;
			this.labelFile.Location = new System.Drawing.Point(57, 13);
			this.labelFile.Name = "labelFile";
			this.labelFile.Size = new System.Drawing.Size(16, 13);
			this.labelFile.TabIndex = 12;
			this.labelFile.Text = "---";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "File: ";
			// 
			// buttonPatch
			// 
			this.buttonPatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonPatch.Location = new System.Drawing.Point(12, 297);
			this.buttonPatch.Name = "buttonPatch";
			this.buttonPatch.Size = new System.Drawing.Size(510, 41);
			this.buttonPatch.TabIndex = 22;
			this.buttonPatch.Text = "Patch!";
			this.buttonPatch.UseVisualStyleBackColor = true;
			this.buttonPatch.Click += new System.EventHandler(this.buttonPatch_Click);
			// 
			// checkBoxPatchAudioThread
			// 
			this.checkBoxPatchAudioThread.AutoSize = true;
			this.checkBoxPatchAudioThread.Checked = true;
			this.checkBoxPatchAudioThread.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxPatchAudioThread.Location = new System.Drawing.Point(97, 236);
			this.checkBoxPatchAudioThread.Name = "checkBoxPatchAudioThread";
			this.checkBoxPatchAudioThread.Size = new System.Drawing.Size(248, 17);
			this.checkBoxPatchAudioThread.TabIndex = 17;
			this.checkBoxPatchAudioThread.Text = "Replace Audio Timing Thread (Recommended)";
			this.checkBoxPatchAudioThread.UseVisualStyleBackColor = true;
			// 
			// checkBoxBgmEnqueueingLogic
			// 
			this.checkBoxBgmEnqueueingLogic.AutoSize = true;
			this.checkBoxBgmEnqueueingLogic.Checked = true;
			this.checkBoxBgmEnqueueingLogic.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxBgmEnqueueingLogic.Location = new System.Drawing.Point(97, 180);
			this.checkBoxBgmEnqueueingLogic.Name = "checkBoxBgmEnqueueingLogic";
			this.checkBoxBgmEnqueueingLogic.Size = new System.Drawing.Size(235, 17);
			this.checkBoxBgmEnqueueingLogic.TabIndex = 25;
			this.checkBoxBgmEnqueueingLogic.Text = "Adjust BGM queueing logic (Recommended)";
			this.checkBoxBgmEnqueueingLogic.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(94, 200);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(421, 26);
			this.label6.TabIndex = 26;
			this.label6.Text = "Fixes the missing BGM bug in the intermission as well as the incorrect BGM being " +
    "played\r\nwhen quickly and repeatedly moving between areas with different music.";
			// 
			// buttonUnpatch
			// 
			this.buttonUnpatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonUnpatch.Location = new System.Drawing.Point(12, 48);
			this.buttonUnpatch.Name = "buttonUnpatch";
			this.buttonUnpatch.Size = new System.Drawing.Size(510, 41);
			this.buttonUnpatch.TabIndex = 27;
			this.buttonUnpatch.Text = "Remove Patches / Restore Original";
			this.buttonUnpatch.UseVisualStyleBackColor = true;
			this.buttonUnpatch.Click += new System.EventHandler(this.buttonUnpatch_Click);
			// 
			// checkBoxAssetPatches
			// 
			this.checkBoxAssetPatches.AutoSize = true;
			this.checkBoxAssetPatches.Checked = true;
			this.checkBoxAssetPatches.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxAssetPatches.Location = new System.Drawing.Point(97, 111);
			this.checkBoxAssetPatches.Name = "checkBoxAssetPatches";
			this.checkBoxAssetPatches.Size = new System.Drawing.Size(213, 17);
			this.checkBoxAssetPatches.TabIndex = 28;
			this.checkBoxAssetPatches.Text = "Apply fixes for known script/asset errors";
			this.checkBoxAssetPatches.UseVisualStyleBackColor = true;
			// 
			// buttonAssetFixDetails
			// 
			this.buttonAssetFixDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAssetFixDetails.Location = new System.Drawing.Point(385, 95);
			this.buttonAssetFixDetails.Name = "buttonAssetFixDetails";
			this.buttonAssetFixDetails.Size = new System.Drawing.Size(137, 44);
			this.buttonAssetFixDetails.TabIndex = 29;
			this.buttonAssetFixDetails.Text = "Show asset fix details... (may contain spoilers)";
			this.buttonAssetFixDetails.UseVisualStyleBackColor = true;
			this.buttonAssetFixDetails.Click += new System.EventHandler(this.buttonAssetFixDetails_Click);
			// 
			// checkBoxFixVoiceFileLang
			// 
			this.checkBoxFixVoiceFileLang.AutoSize = true;
			this.checkBoxFixVoiceFileLang.Checked = true;
			this.checkBoxFixVoiceFileLang.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxFixVoiceFileLang.Location = new System.Drawing.Point(97, 157);
			this.checkBoxFixVoiceFileLang.Name = "checkBoxFixVoiceFileLang";
			this.checkBoxFixVoiceFileLang.Size = new System.Drawing.Size(369, 17);
			this.checkBoxFixVoiceFileLang.TabIndex = 30;
			this.checkBoxFixVoiceFileLang.Text = "Load voice metadata matching voice language rather than text language";
			this.checkBoxFixVoiceFileLang.UseVisualStyleBackColor = true;
			// 
			// Sen2Form
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(534, 350);
			this.Controls.Add(this.checkBoxFixVoiceFileLang);
			this.Controls.Add(this.buttonAssetFixDetails);
			this.Controls.Add(this.checkBoxAssetPatches);
			this.Controls.Add(this.buttonUnpatch);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.checkBoxBgmEnqueueingLogic);
			this.Controls.Add(this.buttonPatch);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.checkBoxPatchAudioThread);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.checkBoxBattleAutoSkip);
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.labelFile);
			this.Controls.Add(this.label1);
			this.Name = "Sen2Form";
			this.Text = "Patch Cold Steel 2";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkBoxBattleAutoSkip;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonPatch;
		private System.Windows.Forms.CheckBox checkBoxPatchAudioThread;
		private System.Windows.Forms.CheckBox checkBoxBgmEnqueueingLogic;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button buttonUnpatch;
		private System.Windows.Forms.CheckBox checkBoxAssetPatches;
		private System.Windows.Forms.Button buttonAssetFixDetails;
		private System.Windows.Forms.CheckBox checkBoxFixVoiceFileLang;
	}
}