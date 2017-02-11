﻿using System;

using WebFormsMvp;
using WebFormsMvp.Web;

using WhenItsDone.MVP.AccountPages.ManageMVP.UploadProfilePictureMVP;

namespace WhenItsDone.WebFormsClient.ViewControls.ManageUserControls
{
    [PresenterBinding(typeof(IUploadProfilePicturePresenter))]
    public partial class UploadProfilePictureUserControl : MvpUserControl<UploadProfilePictureViewModel>, IUploadProfilePictureView
    {
        public event EventHandler<UploadProfilePictureEventArgs> UploadProfilePicture;
        public event EventHandler<UploadProfilePictureFromUrlEventArgs> UploadProfilePictureFromUrl;
        public event EventHandler<UploadProfilePictureInitialStateEventArgs> InitialState;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var loggedUserUsername = Page.User.Identity.Name;
            this.Model.LoggedUserUsername = loggedUserUsername;

            var uploadProfilePictureInitialStateEventArgs = new UploadProfilePictureInitialStateEventArgs(this.Model.LoggedUserUsername);
            this.InitialState?.Invoke(null, uploadProfilePictureInitialStateEventArgs);

            if (!this.Model.IsSuccessful)
            {
                this.DisplayResultError(this.Model.ResultText);
            }
        }

        public void OnUploadProfilePictureButtonClick(object sender, EventArgs args)
        {
            var loggedUserUsername = this.Model.LoggedUserUsername;
            if (this.ProfilePictureFileUpload.HasFile)
            {
                var uploadedFile = this.ProfilePictureFileUpload.FileBytes;
                var uploadedFileName = this.ProfilePictureFileUpload.FileName;

                var uploadProfilePictureEventArgs = new UploadProfilePictureEventArgs(loggedUserUsername, uploadedFileName, uploadedFile);
                this.UploadProfilePicture?.Invoke(null, uploadProfilePictureEventArgs);
            }
            else if (!string.IsNullOrEmpty(this.ProfilePictureUrlTextBox.Text))
            {
                var profilePictureUrl = this.ProfilePictureUrlTextBox.Text;

                var uploadProfilePictureFromUrlEventArgs = new UploadProfilePictureFromUrlEventArgs(loggedUserUsername, profilePictureUrl);
                this.UploadProfilePictureFromUrl?.Invoke(null, uploadProfilePictureFromUrlEventArgs);
            }
            else
            {
                this.DisplayResultError("Something went wrong!");
            }
        }

        private void DisplayResultError(string errorText)
        {
            this.ResultLable.Visible = true;
            this.ResultLable.CssClass = "result-error";
            this.ResultLable.Text = errorText ?? "Something went wrong!";
        }

        private void DisplayResultSuccess(string successText)
        {
            this.ResultLable.Visible = true;
            this.ResultLable.CssClass = "result-success";
            this.ResultLable.Text = successText ?? "Something went right!";
        }

        private void HideResult()
        {
            this.ResultLable.Visible = false;
            this.ResultLable.CssClass = "";
            this.ResultLable.Text = "";
        }
    }
}