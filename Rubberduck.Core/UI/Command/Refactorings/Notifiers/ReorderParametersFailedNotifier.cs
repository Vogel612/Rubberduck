﻿using Rubberduck.Interaction;
using Rubberduck.Parsing.Symbols;
using Rubberduck.Refactorings;
using Rubberduck.Refactorings.Exceptions;

namespace Rubberduck.UI.Command.Refactorings.Notifiers
{
    public class ReorderParametersFailedNotifier : RefactoringFailureNotifierBase
    {
        public ReorderParametersFailedNotifier(IMessageBox messageBox)
            : base(messageBox)
        { }

        protected override string Caption => RefactoringsUI.ReorderParamsDialog_Caption;

        protected override string Message(RefactoringException exception)
        {
            switch (exception)
            {
                case InvalidDeclarationTypeException invalidDeclarationType:
                    Logger.Warn(invalidDeclarationType);
                    return string.Format(RefactoringsUI.RefactoringFailure_InvalidDeclarationType_multipleValid,
                        invalidDeclarationType.TargetDeclaration.QualifiedModuleName,
                        invalidDeclarationType.TargetDeclaration.DeclarationType,
                        $"{DeclarationType.Member}, {DeclarationType.Event}");
                default:
                    return base.Message(exception);
            }
        }
    }
}