using GdeltFilesProcessor.Core.UseCases.ProcessFile;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GdeltFilesProcessor.Tests.Core.UseCases.ProcessFile
{
    public class CsvToGdeltEventConverterTests
    {
        private ICsvToGdeltEventConverter csvToGdeltEventConverter;

        [SetUp]
        public void Setup()
        {
            this.csvToGdeltEventConverter = new CsvToGdeltEventConverter();
        }

        [Test]
        public void Gdelt_event_should_have_correct_id()
        {
            var gdeltEvent = this.csvToGdeltEventConverter.ConvertCsvLineToGdeltEvent("1129", '\t');

            Assert.AreEqual(1129, gdeltEvent.GlobalEventID);
        }
    }
}
