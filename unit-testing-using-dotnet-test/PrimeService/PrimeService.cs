namespace Prime.Services
{
    public class PrimeService
    {
        public bool IsPrime(int number)
        {
            if(number < 2) return false;

            //throw new NotImplementedException("Not implemented.");
            if (number == 2) return true;

            for(int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}