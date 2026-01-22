using System;
using System.Collections.Generic;
using System.Security.Cryptography;

/// <summary>
/// Zusammenfassungsbeschreibung für clsRandomPass
/// </summary>
public class clsRandomPass
{
    //public clsRandomPass()
    //{
    public const string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";
    public const string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
    public const string PASSWORD_CHARS_NUMERIC = "123456789";
    public const string PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/";
    public static string RandomPasswortGenerator()
    {
        string strReturnPass = string.Empty;
        Int32 minLength = 8;
        Int32 maxLength = 10;

        //string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";
        //string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
        //string PASSWORD_CHARS_NUMERIC = "23456789";
        //string PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/";

        // Create a local array containing supported password characters
        // grouped by types. You can remove character groups from this
        // array, but doing so will weaken the password strength.
        char[][] charGroups = new char[][]
        {
                clsRandomPass.PASSWORD_CHARS_LCASE.ToCharArray(),
                clsRandomPass.PASSWORD_CHARS_UCASE.ToCharArray(),
                clsRandomPass.PASSWORD_CHARS_NUMERIC.ToCharArray(),
                clsRandomPass.PASSWORD_CHARS_SPECIAL.ToCharArray()
        };
        // Use this array to track the number of unused characters in each
        // character group.
        int[] charsLeftInGroup = new int[charGroups.Length];
        // Initially, all characters in each group are not used.
        for (int i = 0; i < charsLeftInGroup.Length; i++)
            charsLeftInGroup[i] = charGroups[i].Length;

        // Use this array to track (iterate through) unused character groups.
        int[] leftGroupsOrder = new int[charGroups.Length];

        // Initially, all character groups are not used.
        for (int i = 0; i < leftGroupsOrder.Length; i++)
            leftGroupsOrder[i] = i;

        // Because we cannot use the default randomizer, which is based on the
        // current time (it will produce the same "random" number within a
        // second), we will use a random number generator to seed the
        // randomizer.

        // Use a 4-byte array to fill it with random bytes and convert it then
        // to an integer value.
        byte[] randomBytes = new byte[4];

        // Generate 4 random bytes.
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        rng.GetBytes(randomBytes);

        // Convert 4 bytes into a 32-bit integer value.
        int seed = BitConverter.ToInt32(randomBytes, 0);

        // Now, this is real randomization.
        Random random = new Random(seed);

        // This array will hold password characters.
        char[] password = null;

        // Allocate appropriate memory for the password.
        if (minLength < maxLength)
            password = new char[random.Next(minLength, maxLength + 1)];
        else
            password = new char[minLength];

        // Index of the next character to be added to password.
        int nextCharIdx;

        // Index of the next character group to be processed.
        int nextGroupIdx;

        // Index which will be used to track not processed character groups.
        int nextLeftGroupsOrderIdx;

        // Index of the last non-processed character in a group.
        int lastCharIdx;

        // Index of the last non-processed group.
        int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

        // Generate password characters one at a time.
        for (int i = 0; i < password.Length; i++)
        {
            // If only one character group remained unprocessed, process it;
            // otherwise, pick a random character group from the unprocessed
            // group list. To allow a special character to appear in the
            // first position, increment the second parameter of the Next
            // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
            if (lastLeftGroupsOrderIdx == 0)
                nextLeftGroupsOrderIdx = 0;
            else
                nextLeftGroupsOrderIdx = random.Next(0,
                                                     lastLeftGroupsOrderIdx);

            // Get the actual index of the character group, from which we will
            // pick the next character.
            nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

            // Get the index of the last unprocessed characters in this group.
            lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

            // If only one unprocessed character is left, pick it; otherwise,
            // get a random character from the unused character list.
            if (lastCharIdx == 0)
                nextCharIdx = 0;
            else
                nextCharIdx = random.Next(0, lastCharIdx + 1);

            // Add this character to the password.
            password[i] = charGroups[nextGroupIdx][nextCharIdx];

            // If we processed the last character in this group, start over.
            if (lastCharIdx == 0)
                charsLeftInGroup[nextGroupIdx] =
                                          charGroups[nextGroupIdx].Length;
            // There are more unprocessed characters left.
            else
            {
                // Swap processed character with the last unprocessed character
                // so that we don't pick it until we process all characters in
                // this group.
                if (lastCharIdx != nextCharIdx)
                {
                    char temp = charGroups[nextGroupIdx][lastCharIdx];
                    charGroups[nextGroupIdx][lastCharIdx] =
                                charGroups[nextGroupIdx][nextCharIdx];
                    charGroups[nextGroupIdx][nextCharIdx] = temp;
                }
                // Decrement the number of unprocessed characters in
                // this group.
                charsLeftInGroup[nextGroupIdx]--;
            }

            // If we processed the last group, start all over.
            if (lastLeftGroupsOrderIdx == 0)
                lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
            // There are more unprocessed groups left.
            else
            {
                // Swap processed group with the last unprocessed group
                // so that we don't pick it until we process all groups.
                if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                {
                    int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                    leftGroupsOrder[lastLeftGroupsOrderIdx] =
                                leftGroupsOrder[nextLeftGroupsOrderIdx];
                    leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                }
                // Decrement the number of unprocessed groups.
                lastLeftGroupsOrderIdx--;
            }
        }
        strReturnPass = new string(password);
        return strReturnPass;
    }
    //}
    /// <summary>
    ///             mind. 8 Stellen
    /// </summary>
    /// <param name="myPass"></param>
    /// <returns></returns>
    public static bool CheckPasswort(string myPass)
    {
        bool bReturn = false;
        List<char> ListPass = new List<char>();
        ListPass = myPass.ToCharArray().ToList();

        if (ListPass.Count >= 8)
        {
            bReturn = true;
            List<char> listLCASE = clsRandomPass.PASSWORD_CHARS_LCASE.ToCharArray().ToList();
            List<char> listUCASE = clsRandomPass.PASSWORD_CHARS_UCASE.ToCharArray().ToList();
            List<char> listNUMERIC = clsRandomPass.PASSWORD_CHARS_NUMERIC.ToCharArray().ToList();
            List<char> listSPECIAL = clsRandomPass.PASSWORD_CHARS_SPECIAL.ToCharArray().ToList();

            bool bLCase = false;
            bool bUCASE = false;
            bool bNUMERIC = false;
            bool bSPECIAL = false;

            foreach (char c in ListPass)
            {
                //Kleinbuchstaben
                if ((!bLCase) && (listLCASE.Contains(c)))
                {
                    bLCase = true;
                }
                if ((!bUCASE) && (listUCASE.Contains(c)))
                {
                    bUCASE = true;
                }
                if ((!bNUMERIC) && (listNUMERIC.Contains(c)))
                {
                    bNUMERIC = true;
                }
                if ((!bSPECIAL) && (listSPECIAL.Contains(c)))
                {
                    bSPECIAL = true;
                }
            }
            bReturn = ((bLCase) && (bUCASE) && (bNUMERIC) && (bSPECIAL));
        }
        return bReturn;
    }
}