namespace PhysicalTherapyAPI.Enums
{
    public static class ExerciseTypeExtensions
    {
        private static readonly Dictionary<ExerciseType, string> _exerciseTypeToArabic =
            new Dictionary<ExerciseType, string>
            {
                { ExerciseType.Strengthening, "تقوية" },
                { ExerciseType.Elongated, "إستطالة" },
                { ExerciseType.Balance, "توازن" },
                { ExerciseType.Other, "أخري" }
            };

        public static string GetArabicString(this ExerciseType exerciseType)
        {
            return _exerciseTypeToArabic[exerciseType];
        }

        //to use enum make this(Example):
        //string arabicString = ExerciseType.Strengthening.GetArabicString();

    }
}
