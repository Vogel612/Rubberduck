﻿using System.Collections.ObjectModel;
using System.Linq;
using Rubberduck.Resources;
using Rubberduck.Interaction;
using Rubberduck.Refactorings;
using Rubberduck.Refactorings.ReorderParameters;

namespace Rubberduck.UI.Refactorings.ReorderParameters
{
    public class ReorderParametersPresenter : RefactoringPresenterBase<ReorderParametersModel, IRefactoringDialog<ReorderParametersModel, IRefactoringView<ReorderParametersModel>, IRefactoringViewModel<ReorderParametersModel>>, IRefactoringView<ReorderParametersModel>, IRefactoringViewModel<ReorderParametersModel>>, IReorderParametersPresenter
    {
        private readonly IMessageBox _messageBox;

        public ReorderParametersPresenter(ReorderParametersModel model,
            IRefactoringDialogFactory dialogFactory, IMessageBox messageBox) : base(model, dialogFactory)
        {
            _messageBox = messageBox;
        }

        public override ReorderParametersModel Show()
        {
            if (Model.TargetDeclaration == null) { return null; }

            if (Model.Parameters.Count < 2)
            {
                var message = string.Format(RubberduckUI.ReorderPresenter_LessThanTwoParametersError, Model.TargetDeclaration.IdentifierName);
                _messageBox.NotifyWarn(message, RubberduckUI.ReorderParamsDialog_TitleText);
                return null;
            }

            base.Show();
            if (DialogResult != RefactoringDialogResult.Execute)
            {
                return null;
            }

            return Model;
        }
    }
}
