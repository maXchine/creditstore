namespace Bobby.Libs.Text {

    partial class StringExtensions {

        public static int _2Int(this string s, int defaultValue = 0) {
            int i;
            return int.TryParse(s, out i) ? i : defaultValue;
        }

        public static int? _2NullableInt(this string s) {
            int i;
            return int.TryParse(s, out i) ? (int?) i : null;
        }

        public static bool _2Int(this string s, out int result) {
            return int.TryParse(s, out result);
        }
    }
}
