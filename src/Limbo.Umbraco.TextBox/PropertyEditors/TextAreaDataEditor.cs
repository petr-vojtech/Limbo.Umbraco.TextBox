﻿using System;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Services;

#pragma warning disable CS1591

namespace Limbo.Umbraco.TextBox.PropertyEditors {

    /// <summary>
    /// Represents a textarea property editor.
    /// </summary>
    [DataEditor(EditorAlias, EditorType.PropertyValue, "Limbo Textarea", EditorView, ValueType = ValueTypes.Text, Group = "Limbo", Icon = EditorIcon)]
    public class TextAreaDataEditor : DataEditor {

        public const string EditorAlias = "Limbo.Umbraco.TextArea";

        public const string EditorIcon = "icon-autofill color-limbo";

        public const string EditorView = "/App_Plugins/Limbo.Umbraco.TextBox/Views/TextArea.html";

        private readonly IIOHelper _ioHelper;
        private readonly IEditorConfigurationParser _editorConfigurationParser;
        private readonly TextBoxHelper _helper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextAreaDataEditor"/> class.
        /// </summary>
        public TextAreaDataEditor(IDataValueEditorFactory dataValueEditorFactory, IIOHelper ioHelper, IEditorConfigurationParser editorConfigurationParser, TextBoxHelper helper) : base(dataValueEditorFactory) {
            _ioHelper = ioHelper;
            _editorConfigurationParser = editorConfigurationParser;
            _helper = helper;
        }

        /// <inheritdoc/>
        protected override IDataValueEditor CreateValueEditor() {
            if (Attribute == null) throw new Exception($"'{GetType()}' does not specify a 'DataEditor' attribute");
            TextOnlyValueEditor valueEditor = DataValueEditorFactory.Create<TextOnlyValueEditor>(Attribute);
            if (valueEditor.View?.IndexOf('?') < 0) valueEditor.View += $"?umb__rnd={_helper.GetCacheBuster()}";
            return valueEditor;
        }

        /// <inheritdoc/>
        protected override IConfigurationEditor CreateConfigurationEditor() => new TextAreaConfigurationEditor(_ioHelper, _editorConfigurationParser);

    }

}