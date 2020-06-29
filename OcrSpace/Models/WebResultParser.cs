using ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcrSpace.Models {
    public class WebResultParser {
        private readonly WebResultModel model;

        public WebResultParser(WebResultModel model) {
            if (model == null) {
                throw new ArgumentNullException("Cannot parse null");
            }

            this.model = model;
        }

        public List<string> GetErrors() {          
            List<string> errors = new List<string>();

            if (model.OCRExitCode != 1) {
                if (model.ErrorMessage?.Length == 0) {
                    errors.Add("General unknown error");
                } else {
                    errors.AddRange(model.ErrorMessage);
                }
            }

            foreach (Parsedresult parsedPage in model.ParsedResults) {
                if (parsedPage.ErrorMessage != string.Empty) {
                    errors.Add(parsedPage.ErrorMessage);
                }
            }

            return errors;
        }

        public RecognitionResult Parse() {
            RecognitionResult result = new RecognitionResult();
            result.Errors = GetErrors();

            if (result.Errors.Count > 0) {
                return result;
            }

            result.Pages = ParsePages();
            return result;
        }

        private List<Page> ParsePages() {
            List<Page> pages = new List<Page>();
            foreach (var pageResult in model.ParsedResults) {
                pages.Add(ParsePage(pageResult));
            }

            return pages;
        }

        private Page ParsePage(Parsedresult pageResult) {
            Page page = new Page();
            page.Number = Array.IndexOf(model.ParsedResults, pageResult) + 1;
            page.Words = new List<ApplicationService.Word>();

            var parsedWords = pageResult.TextOverlay.Lines.SelectMany(line => line.Words);

            page.Words = parsedWords.Select(
                word => new ApplicationService.Word() {
                    Text = word.WordText,
                    Rectangle = new System.Drawing.Rectangle((int)word.Left, (int)word.Top, (int)word.Width, (int)word.Height)
                }).ToList();

            return page;
        }
    }
}
