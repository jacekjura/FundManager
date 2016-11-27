using FluentAssertions;
using FundManager.Converters;
using NUnit.Framework;
using System;
using System.Windows.Media;

namespace FundManager.Test.Converters
{
    [TestFixture]
    public class BooleanToBrushConverterTest
    {
        [Test]
        [TestCase(true, "#FFFF0000")]
        [TestCase(false, "#FF000000")]
        public void Convert_Boolean_CorrectBrush(bool input, string output)
        {
            var subject = new BooleanToBrushConverter();
            var result = subject.Convert(input, typeof(bool), input, null) as SolidColorBrush;
            result.Color.ToString().Should().Be(output);
        }

        [Test]
        public void ShouldThrowNotImplementedExceptionInConvertBack()
        {
            var subject = new BooleanToBrushConverter();
            subject.Invoking(p => p.ConvertBack(null, null, null, null)).ShouldThrow<NotImplementedException>();
        }
    }
}
