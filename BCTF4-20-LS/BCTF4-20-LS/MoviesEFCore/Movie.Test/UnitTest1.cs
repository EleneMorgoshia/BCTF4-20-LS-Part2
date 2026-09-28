namespace Movie.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //AAA - PRINCIPLE
            //ARRAGMENT
            int a = 5;
            int b = 10;

            //ACT
            int sum = a + b;


            //ASSERT
            Assert.Equal(15, sum);
        }
    }
}