﻿using System.Collections.Generic;
using Rubberduck.Inspections.Abstract;
using Rubberduck.Inspections.Concrete;
using Rubberduck.Parsing.Inspections.Abstract;
using Rubberduck.Parsing.Rewriter;
using Rubberduck.Parsing.Symbols;
using Rubberduck.Parsing.VBA;
using Rubberduck.Parsing.VBA.Parsing;

namespace Rubberduck.Inspections.QuickFixes
{
    /// <summary>
    /// Removes a hidden attribute, in order to maintain consistency between hidden attributes and (missing) annotation comments.
    /// </summary>
    /// <inspections>
    /// <inspection name="MissingModuleAnnotationInspection" />
    /// <inspection name="MissingMemberAnnotationInspection" />
    /// </inspections>
    /// <canfix procedure="false" module="false" project="false" />
    /// <example>
    /// <before>
    /// <![CDATA[
    /// Option Explicit
    /// 
    /// Public Sub DoSomething()
    /// Attribute VB_UserMemId = 0
    /// 
    /// End Sub
    /// ]]>
    /// </before>
    /// <after>
    /// <![CDATA[
    /// Option Explicit
    /// 
    /// Public Sub DoSomething()
    /// 
    /// End Sub
    /// ]]>
    /// </after>
    /// </example>
    public class RemoveAttributeQuickFix : QuickFixBase
    {
        private readonly IAttributesUpdater _attributesUpdater;

        public RemoveAttributeQuickFix(IAttributesUpdater attributesUpdater)
        :base(typeof(MissingModuleAnnotationInspection), typeof(MissingMemberAnnotationInspection))
        {
            _attributesUpdater = attributesUpdater;
        }

        public override void Fix(IInspectionResult result, IRewriteSession rewriteSession)
        {
            var declaration = result.Target;
            string attributeBaseName = result.Properties.AttributeName; 
            IReadOnlyList<string> attributeValues = result.Properties.AttributeValues;

            var attributeName = declaration.DeclarationType.HasFlag(DeclarationType.Module)
                ? attributeBaseName
                : $"{declaration.IdentifierName}.{attributeBaseName}";

            _attributesUpdater.RemoveAttribute(rewriteSession, declaration, attributeName, attributeValues);
        }

        public override string Description(IInspectionResult result) => Resources.Inspections.QuickFixes.RemoveAttributeQuickFix;

        public override CodeKind TargetCodeKind => CodeKind.AttributesCode;

        public override bool CanFixInProcedure => false;
        public override bool CanFixInModule => false;
        public override bool CanFixInProject => false;
    }
}