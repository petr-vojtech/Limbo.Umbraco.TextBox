﻿using System;
using Skybrud.Essentials.Strings;
using Umbraco.Cms.Core.Dictionary;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;

#pragma warning disable CS1591

namespace Limbo.Umbraco.TextBox.PropertyEditors.ValueConverters {

    public class TextAreaValueConverter : PropertyValueConverterBase {

        private readonly ILocalizedTextService _localizedTextService;
        private readonly ICultureDictionary _cultureDictionary;

        public TextAreaValueConverter(ILocalizedTextService localizedTextService, ICultureDictionary cultureDictionary) {
            _localizedTextService = localizedTextService;
            _cultureDictionary = cultureDictionary;
        }

        public override bool IsConverter(IPublishedPropertyType propertyType) {
            return propertyType.EditorAlias == TextAreaDataEditor.EditorAlias;
        }

        public override object? ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object? source, bool preview) {
            return source;
        }

        public override object ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object? inter, bool preview) {

            if (propertyType.DataType.Configuration is not TextAreaConfiguration config) return string.Empty;

            string? value = inter as string;

            if (string.IsNullOrWhiteSpace(value)) {
                return config.Fallback.IsNullOrWhiteSpace() ? string.Empty : _localizedTextService.UmbracoDictionaryTranslate(_cultureDictionary, config.Fallback) ?? string.Empty;
            }

            return config.StripHtml ? StringUtils.StripHtml(value) : value;

        }

        public override Type GetPropertyValueType(IPublishedPropertyType propertyType) {
            return typeof(string);
        }

    }

}