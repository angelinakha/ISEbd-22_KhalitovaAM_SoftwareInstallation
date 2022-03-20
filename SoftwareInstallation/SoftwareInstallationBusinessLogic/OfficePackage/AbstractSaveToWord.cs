﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareInstallationBusinessLogic.OfficePackage.HelperEnums;
using SoftwareInstallationBusinessLogic.OfficePackage.HelperModels;

namespace SoftwareInstallationBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDoc(WordInfo info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { 
                    Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            foreach (var component in info.Components)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> {
                        (component.ComponentName, new WordTextProperties { Size = "24", }) },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }
            SaveWord(info);
        }

        // Создание doc-файла
        protected abstract void CreateWord(WordInfo info);
        // Создание абзаца с текстом
        protected abstract void CreateParagraph(WordParagraph paragraph);
        // Сохранение файла
        protected abstract void SaveWord(WordInfo info);

    }
}
