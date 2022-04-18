namespace Task5_Term4;

public class BigInteger
{
    private int[] _numbers;
    private bool positive = true;

    public BigInteger(string value)
    {
        if (value[0] == '-')
        {
            positive = false;
            value = value.Remove(0, 1);
        }
        _numbers = new int[value.Length];
        for(int i = 0; i < value.Length; i++)
        {
            _numbers[i] = (int)value[i];
        }
    }

    public override string ToString()
    {
        string value = "";
        if (!positive)
        {
            value = value + "-";
        }
        for (int i = 0; i < _numbers.Length; i++)
        {
            value = value + (char)_numbers[i];
        }
        return value;
    }

    public BigInteger Add(BigInteger another)
    {
        string result = "";
        string firstNum = "";
        for (int i = 0; i < _numbers.Length; i++)
        {
            firstNum = firstNum + (char)_numbers[i];
        }
        string secNum = another.ToString();
        if (firstNum.Length > secNum.Length)
        {
            int difference = firstNum.Length - secNum.Length;
            for (int i = 0; i < difference; i++)
            {
                secNum = "0" + secNum;
            }
        }
        else if (secNum.Length > firstNum.Length)
        {
            int difference = secNum.Length - firstNum.Length;
            for (int i = 0; i < difference; i++)
            {
                firstNum = "0" + firstNum;
            }
        }
        string toCount = "";
        for (int i = secNum.Length - 1; i >= 0; i--)
        {
            if (toCount != "")
            {
                for (int y = i; y >= -1; y--)
                {
                    if (y == -1)
                    {
                        firstNum = '1' + firstNum;
                        secNum = '0' + secNum;
                        i += 1;
                        break;
                    }
                    if (firstNum[y] != '9')
                    {
                        int digit = firstNum[y] - '0' + 1;
                        firstNum = replaceByIndex(firstNum, y, Convert.ToChar(digit.ToString()));
                        break;
                    }
                    else
                    {
                        firstNum = replaceByIndex(firstNum, y, '0');
                    }
                }
            }
            toCount = Convert.ToString(firstNum[i] - '0' + secNum[i] - '0');
            result = toCount[toCount.Length - 1] + result;
            toCount = toCount.Remove(0, 1);
        }
        if (toCount != "")
        {
            result = '1' + result;
        }
        return new BigInteger(result);
    }


    public string replaceByIndex(string str, int index, char symbol)
    {
        string result = "";
        for (int i = 0; i < str.Length; i++)
        {
            if (i != index)
            {
                result += str[i];
            }
            else
            {
                result += symbol;
            }
        }
        return result;
    }
}