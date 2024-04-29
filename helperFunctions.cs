namespace helperFunctions;

public class HelperFunctions {
    
    public static bool VerifyDateFormat(string date) {
        return date.Length == 10 && date[2] == '/' && date[5] == '/';
    }

    public static bool VerifyIntInput(string dataToBeVerified) {
        if(int.TryParse(dataToBeVerified, out int value)) {
            return true;
        }
        return false;
    }

    public static bool VerifyBoolInput(string dataToBeVerified) {
        if(bool.TryParse(dataToBeVerified, out bool value)) {
            return true;
        }
        return false;
    }

}