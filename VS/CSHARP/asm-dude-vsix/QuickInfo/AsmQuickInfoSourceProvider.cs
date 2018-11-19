﻿// The MIT License (MIT)
//
// Copyright (c) 2018 Henk-Jan Lebbink
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

using AsmDude.Tools;

namespace AsmDude.QuickInfo
{
    /// <summary>
    /// Factory for quick info sources
    /// </summary>
    [Export(typeof(IAsyncQuickInfoSourceProvider))]
    [ContentType(AsmDudePackage.AsmDudeContentType)]
    [TextViewRole(PredefinedTextViewRoles.Debuggable)]
    [Name("AsmQuickInfoSourceProvider")]
    internal sealed class AsmQuickInfoSourceProvider : IAsyncQuickInfoSourceProvider
    {
        [Import]
        private readonly IBufferTagAggregatorFactoryService _aggregatorFactory = null;

        [Import]
        private readonly ITextDocumentFactoryService _docFactory = null;

        [Import]
        private readonly IContentTypeRegistryService _contentService = null;

        public IAsyncQuickInfoSource TryCreateQuickInfoSource(ITextBuffer buffer)
        {
            AsmQuickInfoSource localFunction()
            {
                var labelGraph = AsmDudeToolsStatic.GetOrCreate_Label_Graph(buffer, this._aggregatorFactory, this._docFactory, this._contentService);
                var asmSimulator = AsmSimulator.GetOrCreate_AsmSimulator(buffer, this._aggregatorFactory);
                return new AsmQuickInfoSource(buffer, this._aggregatorFactory, labelGraph, asmSimulator);
            }
            return buffer.Properties.GetOrCreateSingletonProperty(localFunction);
        }
    }
}
