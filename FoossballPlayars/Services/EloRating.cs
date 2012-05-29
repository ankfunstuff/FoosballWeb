using System;

namespace FoossballPlayars.Services
{
    public class EloRating
    {
        private const int KFactor = 400;
        private const int EFactor = 60;
        public double Point1 { get; private set; }
        public double Point2 { get; private set; }

        public EloRating(double currentRating1, double currentRating2, int score1, int score2, int gamesPrPlayer)
        {
            double e;
            double finalResult1;
            double finalResult2;
            if (Math.Abs(score1 - score2) > 0)
            {
                if (score1 > score2)
                {
                    e = EFactor - Math.Round(1 / (1 + Math.Pow(10, ((currentRating2 - currentRating1) / KFactor))) * EFactor);
                    finalResult1 = currentRating1 + e;
                    finalResult2 = currentRating2 - e;
                }
                else
                {
                    e = EFactor - Math.Round(1 / (1 + Math.Pow(10, ((currentRating1 - currentRating2) / KFactor))) * EFactor);
                    finalResult1 = currentRating1 - e;
                    finalResult2 = currentRating2 + e;
                }
            }
            else
            {
                if (Math.Abs(currentRating1 - currentRating2) < 0)
                {
                    finalResult1 = currentRating1;
                    finalResult2 = currentRating2;
                }
                else
                {
                    if (currentRating1 > currentRating2)
                    {
                        e = (EFactor - Math.Round(1 / (1 + Math.Pow(10, ((currentRating1 - currentRating2) / KFactor))) * EFactor)) - (EFactor - Math.Round(1 / (1 + Math.Pow(10, ((currentRating2 - currentRating1) / KFactor))) * EFactor));
                        finalResult1 = currentRating1 - e;
                        finalResult2 = currentRating2 + e;
                    }
                    else
                    {
                        e = (EFactor - Math.Round(1 / (1 + Math.Pow(10, ((currentRating2 - currentRating1) / KFactor))) * EFactor)) - (EFactor - Math.Round(1 / (1 + Math.Pow(10, ((currentRating1 - currentRating2) / KFactor))) * EFactor));
                        finalResult1 = currentRating1 + e;
                        finalResult2 = currentRating2 - e;
                    }
                }
            }
            Point1 = finalResult1 - currentRating1;
            Point2 = finalResult2 - currentRating2;
        }
    }
}
