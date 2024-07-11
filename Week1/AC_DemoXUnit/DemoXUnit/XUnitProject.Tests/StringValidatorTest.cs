namespace XUnitProject.Tests;

public class StringValidatorTest {
    [Fact]
    public void TestIsStringEmpty() {
        StringValidator validator = new StringValidator();
        Assert.False(validator.IsEmpty("foo"));
    }

    [Theory]
    [InlineData("", true)]
    [InlineData("MyString", false)]
    [InlineData("MyString and me", false)]
    public void TestIsStringWhitespace(string pInput, bool pExpected) {
        StringValidator validator = new StringValidator();
        Assert.Equal(pExpected, validator.IsEmpty(pInput));
    }
}