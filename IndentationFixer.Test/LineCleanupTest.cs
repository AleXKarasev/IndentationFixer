using NUnit.Framework;
using Shouldly;

namespace IndentationFixer.Test
{
    [TestFixture]
    public class LineCleanupTest
    {
        [Test]
        public void CorrectLineTest()
        {
            var inputLine = "   {serserse sdgf ;}sdfg sdg sg ;";
            var target = new LineCleanup(inputLine);
            target.CleanEmptyLines();

            target.IsChanged.ShouldBeFalse();
            target.InternalString.ShouldBe(inputLine);
        }


        [Test]
        public void EmptyLineReplacementTest()
        {
            var target = new LineCleanup("   ");
            target.CleanEmptyLines();

            target.IsChanged.ShouldBeTrue();
            target.InternalString.ShouldBe(string.Empty);
        }

        [Test]
        public void RemoveEndSpacesTest()
        {
            var target = new LineCleanup("werw wertw ertwet;   ");
            target.RemoveEndSpaces();

            target.IsChanged.ShouldBeTrue();
            target.InternalString.ShouldBe("werw wertw ertwet;");
        }

        [Test]
        public void ReplaceTabToSpacesTest()
        {
            var target = new LineCleanup(" \t \t werw wertw ertwet;");
            target.ReplaceTabToSpaces();

            target.IsChanged.ShouldBeTrue();
            target.InternalString.ShouldBe("           werw wertw ertwet;");
        }
    }
}