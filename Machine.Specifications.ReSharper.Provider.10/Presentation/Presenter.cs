﻿using JetBrains.ReSharper.Feature.Services.Tree;

namespace Machine.Specifications.ReSharperProvider.Presentation
{
    using System.Drawing;

    using JetBrains.CommonControls;
    using JetBrains.ReSharper.UnitTestFramework;
    using JetBrains.ReSharper.UnitTestFramework.UI;
    using JetBrains.TreeModels;
    using JetBrains.UI.TreeView;

    class Presenter : TreeModelBrowserPresenter
    {
        public Presenter()
        {
            this.Present<ContextElement>(this.PresentContext);
            this.Present<FieldElement>(this.PresentSpecification);
            this.Present<BehaviorElement>(this.PresentBehavior);
            this.Present<BehaviorSpecificationElement>(this.PresentBehaviorSpecification);
        }

        protected virtual void PresentContext(ContextElement element,
                                              IPresentableItem item,
                                              TreeModelNode modelNode,
                                              PresentationState state)
        {
            PresentItem(item, element, state, UnitTestElementImage.TestContainer);
        }

        protected virtual void PresentSpecification(FieldElement element,
                                                    IPresentableItem item,
                                                    TreeModelNode modelNode,
                                                    PresentationState state)
        {
            PresentItem(item, element, state, UnitTestElementImage.Test);
        }

        protected virtual void PresentBehavior(BehaviorElement element,
                                               IPresentableItem item,
                                               TreeModelNode modelNode,
                                               PresentationState state)
        {
            PresentItem(item, element, state, UnitTestElementImage.TestContainer);
        }

        protected virtual void PresentBehaviorSpecification(BehaviorSpecificationElement element,
                                                            IPresentableItem item,
                                                            TreeModelNode modelNode,
                                                            PresentationState state)
        {
            PresentItem(item, element, state, UnitTestElementImage.Test);
        }

        static void PresentItem(IPresentableItem item, Element element, PresentationState state, UnitTestElementImage type)
        {
            item.RichText = element.GetPresentation();

            SetTextColor(item, element);
        }

        static void SetTextColor(IPresentableItem item, Element element)
        {
            if (element.Explicit)
            {
                item.RichText.SetForeColor(SystemColors.GrayText);
            }

            item.RichText.SetForeColor(SystemColors.GrayText, 0, element.GetTitlePrefix().Length);
        }

        protected override bool IsNaturalParent(object parentValue, object childValue)
        {
            var @namespace = parentValue as UnitTestElementNamespace;
            var context = childValue as ContextElement;

            if (context != null && @namespace != null)
            {
                return @namespace.Equals(context.GetNamespace());
            }

            return base.IsNaturalParent(parentValue, childValue);
        }

        protected override object Unwrap(object value)
        {
            var specification = value as FieldElement;
            if (specification != null)
            {
                value = specification.GetDeclaredElement();
            }

            var context = value as ContextElement;
            if (context != null)
            {
                value = context.GetDeclaredElement();
            }
            return base.Unwrap(value);
        }

    }
}