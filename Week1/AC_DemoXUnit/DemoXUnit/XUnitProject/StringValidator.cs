namespace XUnitProject;

public class StringValidator {
    //  MainMethod - Is Empty (param String)
    /// <summary>
    /// Returns if the string is empty or not
    /// </summary>
    /// <param name="pStr">String to check</param>
    /// <returns></returns>
    public bool IsEmpty(string pStr) {
        return string.IsNullOrEmpty(pStr);
    }
}