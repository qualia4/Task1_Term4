namespace Task5_Term4;

public class BigInteger
{
    private int[] _numbers;
    private bool positive { get; } = true;

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
        if (positive && !another.positive)
            return this.Sub(new BigInteger(another.ToString().Remove(0, 1)));
        if (!positive && another.positive)
            return another.Sub(new BigInteger(this.ToString().Remove(0, 1)));
        if (!positive && !another.positive)
        {
            BigInteger modAnother = new BigInteger(another.ToString().Remove(0, 1));
            BigInteger modFirst = new BigInteger(this.ToString().Remove(0, 1));
            return new BigInteger("-" + modFirst.Add(modAnother).ToString());
        }
        string result = "";
        string firstNum = this.ToString();
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



    public BigInteger Sub(BigInteger another)
    {
        if (positive && !another.positive)
            return this.Add(new BigInteger(another.ToString().Remove(0, 1)));
        if (!positive && another.positive)
            return new BigInteger("-" + another.Add(new BigInteger(this.ToString().Remove(0, 1))));
        if (!positive && !another.positive)
        {
            BigInteger modAnother = new BigInteger(another.ToString().Remove(0, 1));
            BigInteger modFirst = new BigInteger(this.ToString().Remove(0, 1));
            return modAnother.Sub(modFirst);
        }

        string result = "";
        string firstNum;
        string secNum;
        bool negative = false;
        if (findBigger(this.ToString(), another.ToString()) == this.ToString())
        {
            firstNum = this.ToString();
            secNum = another.ToString();
        }
        else
        {
            negative = true;
            firstNum = another.ToString();
            secNum = this.ToString();
        }
        if (firstNum.Length > secNum.Length)
        {
            int difference = firstNum.Length - secNum.Length;
            for (int i = 0; i < difference; i++)
            {
                secNum = "0" + secNum;
            }
        }
        bool toTake = false;
        for (int i = secNum.Length - 1; i >= 0; i--)
        {
            int takeIndex = i;
            while (toTake)
            {
                if (firstNum[takeIndex] - '0' >= 1)
                {
                    int newDigit = firstNum[takeIndex] - '0' - 1;
                    firstNum = replaceByIndex(firstNum, takeIndex, Convert.ToChar(newDigit.ToString()));
                    toTake = false;
                }
                else
                {
                    firstNum = replaceByIndex(firstNum, takeIndex, '9');
                    takeIndex--;
                }
            }
            int diff;
            if (firstNum[i] - '0' >= secNum[i] - '0')
            {
                diff = (firstNum[i] - '0') - (secNum[i] - '0');
                result = diff.ToString() + result;
            }
            else
            {
                toTake = true;
                string modifiedFirst = "1" + firstNum[i];
                diff = Int32.Parse(modifiedFirst) - (secNum[i] - '0');
                result = diff.ToString() + result;
            }
        }

        while (result[0] == '0')
        {
            result = result.Remove(0, 1);
        }
        return negative ? new BigInteger("-" + result) : new BigInteger(result);
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

    public string findBigger(string num1, string num2)
    {
        if (num1.Length > num2.Length)
            return num1;
        else if (num2.Length > num1.Length)
            return num2;
        else
        {
            for (int i = 0; i < num1.Length; i++)
            {
                if (num1[i] - '0' > num2[i] - '0')
                    return num1;
            }
        }
        return num2;
    }

    public static BigInteger operator +(BigInteger a, BigInteger b) => a.Add(b);
    public static BigInteger operator -(BigInteger a, BigInteger b) => a.Sub(b);
}