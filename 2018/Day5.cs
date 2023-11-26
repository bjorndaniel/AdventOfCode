char[,] alphabetArray = new char[26, 2];
for (int i = 0; i < 26; i++)
{
    alphabetArray[i, 0] = (char)(97 + i); // lowercase letter
    alphabetArray[i, 1] = (char)(65 + i); // uppercase letter
}
