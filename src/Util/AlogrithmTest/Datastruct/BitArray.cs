﻿/**
 * Author: Christian Bender
 * Class: BitArray
 * 
 * implements IComparable, ICloneable, IEnumerator, IEnumerable
 * 
 * This class implements a bit-array and provides some
 * useful functions/operations to deal with this type of
 * data structure. You see a overview about the functionality, below.
 * 
 * 
 * 					Overview
 * 
 * Constructor (N : int)
 * 	The constructor receives a length (N) of the to create bit-field.
 * 
 * Constructor (sequence : string)
 * 	setups the array with the input sequence.
 * 	assumes: the sequence may only be allowed contains onese or zeros.
 * 
 * Constructor (bits : bool[] ) 
 * 	setups the bit-field with the input array.
 * 
 * Compile(sequence : string)
 * 	compiles a string sequence of 0's and 1's in the inner structure.
 * 	assumes: the sequence may only be allowed contains onese or zeros.
 * 
 * Compile (number : int)
 * 	compiles a positive integer number in the inner data structure.
 * 
 * Compile (number : long)
 * 	compiles a positive long integer number in the inner data structure.
 * 
 * ToString ()
 * 	returns a string representation of the inner structure. 
 * 	The returned string is a sequence of 0's and 1's.
 * 
 * Length : int
 * 	Is a property that returns the length of the bit-field.
 * 
 * Indexer : bool
 * 	indexer for selecting the individual bits of the bit array.
 * 
 * NumberOfOneBits() : int
 * 	returns the number of One-bits.
 * 
 * NumberOfZeroBits() : int
 * 	returns the number of Zero-Bits.
 * 
 * EvenParity() : bool
 * 	returns true if parity is even, otherwise false.
 * 
 * OddParity() : bool
 * 	returns true if parity is odd, otherwise false.
 * 
 * ToInt64() : long
 *	returns a long integer representation of the bit-array.
 *	assumes: the bit-array length must been smaller or equal to 64 bit. 
 *
 * ToInt32() : int
 *	returns a integer representation of the bit-array.
 *	assumes: the bit-array length must been smaller or equal to 32 bit. 
 *
 * ResetField() : void
 * 	sets all bits on false.
 * 
 * SetAll(flag : bool) : void
 * 	sets all bits on the value of the flag.
 * 
 * GetHashCode() : int
 * 	returns hash-code (ToInt32())
 * 
 * Equals (other : Object) : bool
 * 	returns true if there inputs are equal otherwise false.
 * 	assumes: the input bit-arrays must have same length.
 * 
 * CompareTo (other : Object) : int  (interface IComparable)
 * 	output:  0 - if the bit-arrays a equal.
 * 		  	-1 - if this bit-array is smaller.
 * 			 1 - if this bit-array is greater.
 *  assumes: bit-array lentgh must been smaller or equal to 64 bit
 *
 * Clone () : object
 * 	returns a copy of this bit-array
 * 
 * Current : object
 * 	returns the current selected bit.
 * 
 * MoveNext() : bool
 * 	purpose: increases the position of the enumerator
 * 	returns true if 'position' successful increased otherwise false.
 * 
 * Reset() : void 
 * 	resets the position of the enumerator.
 * 
 * GetEnumerator() : IEnumerator
 * 	returns a enumerator for this BitArray-object.
 * 
 * Operations:
 * 
 * 	& bitwise AND
 * 	| bitwise OR
 * 	~ bitwise NOT
 * 	>> bitwise shift right
 * 	<< bitwise shift left
 *  ^ bitwise XOR
 * 
 * Each operation (above) returns a new BitArray-object. 
 * 
 * 	== equal operator. : bool
 * 		returns true if there inputs are equal otherwise false.
 * 		assumes: the input bit-arrays must have same length.
 * 
 * 	!= not-equal operator : bool
 * 		returns true if there inputs aren't equal otherwise false.
 *		assumes: the input bit-arrays must have same length.
 * 
 * */


using System;
using System.Collections;

/// <summary>
/// 位图
/// </summary>
namespace DataStructures.BitArray
{
    public class BitArray : IComparable, ICloneable, IEnumerator, IEnumerable
    {
        /// <summary>
        /// bitArray实际存储
        /// </summary>
        private readonly bool[] _field;      // the actual bit-field
        /// <summary>
        /// 位置参数
        /// </summary>
        private int position = -1;  // position for enumerator

         /// <summary>
         /// 构造包含n个位的bitArray,默认位值为false
         /// </summary>
         /// <param name="N"></param>
        public BitArray(int N)
        {
            if (N >= 1)
            {

                _field = new bool[N];

                // fills up the field with zero-bits.
                for (var i = 0; i < N; i++)
                {
                    _field[i] = false;
                }

            }
            else
            { // error case

                throw new Exception("BitArray: N must been greater or equal to 1");

            }
        }

         /// <summary>
         /// 根据字符串构造bitArray，若字符为0或1位值为true，否则为false
         /// </summary>
         /// <param name="sequence"></param>
        public BitArray(string sequence)
        {

            // precondition I
            if (sequence.Length > 0)
            {


                // precondition II
                if (Match(sequence))
                {


                    _field = new bool[sequence.Length];
                    Compile(sequence);

                }
                else
                { // error case II

                    throw new Exception("BitArray: the sequence may only " +
                    "be allowed contains onese or zeros.");

                }

            }
            else
            { // error case I

                throw new Exception("BitArray: sequence must been greater or equal as 1");

            }
        }

        /*
		 * constructor
		 * input: a boolean array of bits.  
		 * output: none
		 * purpose: setups the bit-array with the input array.
		 * */
        public BitArray(bool[] bits) => _field = bits;

        /*
		 * Compile
		 * input: a string sequence of 0's and 1's
		 * output: none
		 * purpose: compiles the binary sequence into the inner data structure.
		 * assumes: the sequence must have the same length, as the bit-array.
		 * 			the sequence may only be allowed contains onese or zeros.
		 * */
        public void Compile(string sequence)
        {
            var tmp = "";

            sequence = sequence.Trim();

            // precondition I
            if (sequence.Length <= _field.Length)
            {

                // precondition II
                if (Match(sequence))
                {


                    // for appropriate scaling
                    if (sequence.Length < _field.Length)
                    {
                        var difference = _field.Length - sequence.Length;

                        for (var i = 0; i < difference; i++)
                        {
                            tmp += '0';
                        }

                        tmp += sequence;
                        sequence = tmp;
                    }

                    // actual compile procedure. 
                    for (var i = 0; i < sequence.Length; i++)
                    {
                        _field[i] = sequence[i] == '1';
                    }
                }
                else
                { // error case II
                    throw new Exception("Compile: the sequence may only " +
                    "be allowed contains onese or zeros.");
                }

            }
            else
            { // error case I
                throw new Exception("Compile: not equal length!");
            }
        }

        /**
		 * Compile
		 * input: an positive integer number
		 * output: none
		 * purpose: compiles integer number into the inner data structure.
		 * assumes: the number must have the same bit length.
		 * */
        public void Compile(int number)
        {
            var tmp = "";

            // precondition I
            if (number > 0)
            {

                // converts to binary representation 转换成二进制形式
                var binaryNumber = Convert.ToString(number, 2);

                // precondition II
                if (binaryNumber.Length <= _field.Length)
                {

                    // for appropriate scaling
                    if (binaryNumber.Length < _field.Length)
                    {

                        var difference = _field.Length - binaryNumber.Length;

                        for (var i = 0; i < difference; i++)
                        {
                            tmp += '0';
                        }

                        tmp += binaryNumber;
                        binaryNumber = tmp;
                    }

                    // actual compile procedure. 
                    for (var i = 0; i < binaryNumber.Length; i++)
                    {
                        _field[i] = binaryNumber[i] == '1';
                    }

                }
                else
                { // error case II
                    throw new Exception("Compile: not apt length!");
                }

            }
            else
            { // error case I
                throw new Exception("Compile: only positive numbers > 0");
            }
        }


        /**
		 * Compile
		 * input: an positive long integer number
		 * output: none
		 * purpose: compiles integer number into the inner data structure.
		 * assumes: the number must have the same bit length.
		 * */
        public void Compile(long number)
        {
            var tmp = "";

            // precondition I
            if (number > 0)
            {

                // converts to binary representation
                var binaryNumber = Convert.ToString(number, 2);

                // precondition II
                if (binaryNumber.Length <= _field.Length)
                {

                    // for appropriate scaling
                    if (binaryNumber.Length < _field.Length)
                    {

                        var difference = _field.Length - binaryNumber.Length;

                        for (var i = 0; i < difference; i++)
                        {
                            tmp += '0';
                        }

                        tmp += binaryNumber;
                        binaryNumber = tmp;

                    }

                    // actual compile procedure. 
                    for (var i = 0; i < binaryNumber.Length; i++)
                    {

                        _field[i] = binaryNumber[i] == '1';

                    }

                }
                else
                { // error case II

                    throw new Exception("Compile: not apt length!");

                }

            }
            else
            { // error case I

                throw new Exception("Compile: only positive numbers > 0");
            }
        }


/// <summary>
/// 转换为二进制字符串
/// </summary>
/// <returns></returns>
        public override string ToString()
        {
            var ans = "";

            // creates return-string
            for (var i = 0; i < _field.Length; i++)
            {

                if (_field[i])
                {

                    ans += "1";
                }
                else
                {

                    ans += "0";
                }

            }

            return ans;

        }

        /**
		 * Property
		 * Length: returns the length of the current bit array.
		 * */
        public int Length => _field.Length;

/// <summary>
/// 与操作
/// </summary>
/// <param name="one"></param>
/// <param name="two"></param>
/// <returns></returns>
        public static BitArray operator &(BitArray one, BitArray two)
        {
            var sequence1 = one.ToString();
            var sequence2 = two.ToString();
            var result = "";
            var tmp = "";

            // for scaling of same length.
            if (one.Length != two.Length)
            {
                int difference;
                if (one.Length > two.Length)
                { // one is greater

                    difference = one.Length - two.Length;

                    // fills up with 0's
                    for (var i = 0; i < difference; i++)
                    {
                        tmp += '0';
                    }

                    tmp += two.ToString();
                    sequence2 = tmp;

                }
                else
                { // two is greater

                    difference = two.Length - one.Length;

                    // fills up with 0's
                    for (var i = 0; i < difference; i++)
                    {
                        tmp += '0';
                    }

                    tmp += one.ToString();
                    sequence1 = tmp;


                }

            } // end scaling

            var ans = new BitArray(one.Length);

            for (var i = 0; i < one.Length; i++)
            {

                switch (sequence1[i])
                {

                    case '0':
                        result += '0';
                        break;
                    case '1':
                        if (sequence2[i] == '1')
                        {
                            result += '1';
                        }
                        else
                        {
                            result += '0';
                        }
                        break;

                }

            }

            result = result.Trim();
            ans.Compile(result);

            return ans;

        }


/// <summary>
/// 或操作
/// </summary>
/// <param name="one"></param>
/// <param name="two"></param>
/// <returns></returns>
        public static BitArray operator |(BitArray one, BitArray two)
        {
            var sequence1 = one.ToString();
            var sequence2 = two.ToString();
            var result = "";
            var tmp = "";

            // for scaling of same length.
            if (one.Length != two.Length)
            {
                int difference;
                if (one.Length > two.Length)
                { // one is greater

                    difference = one.Length - two.Length;

                    // fills up with 0's
                    for (var i = 0; i < difference; i++)
                    {
                        tmp += '0';
                    }

                    tmp += two.ToString();
                    sequence2 = tmp;

                }
                else
                { // two is greater

                    difference = two.Length - one.Length;

                    // fills up with 0's
                    for (var i = 0; i < difference; i++)
                    {
                        tmp += '0';
                    }

                    tmp += one.ToString();
                    sequence1 = tmp;


                }

            } // end scaling

            var ans = new BitArray(one.Length);

            for (var i = 0; i < one.Length; i++)
            {

                switch (sequence1[i])
                {

                    case '0':
                        if (sequence2[i] == '1')
                        {
                            result += '1';
                        }
                        else
                        {
                            result += '0';
                        }
                        break;
                    case '1':
                        result += '1';
                        break;

                }

            }

            result = result.Trim();
            ans.Compile(result);


            return ans;

        }

/// <summary>
/// 取反
/// </summary>
/// <param name="one"></param>
/// <returns></returns>
        public static BitArray operator ~(BitArray one)
        {

            var ans = new BitArray(one.Length);
            var sequence = one.ToString();
            var result = "";

            foreach (var ch in sequence)
            {
                if (ch == '1')
                {
                    result += '0';
                }
                else
                {
                    result += '1';
                }
            }

            result = result.Trim();
            ans.Compile(result);

            return ans;

        }



/// <summary>
/// 左移
/// </summary>
/// <param name="other"></param>
/// <param name="n"></param>
/// <returns></returns>
        public static BitArray operator <<(BitArray other, int n)
        {

            var ans = new BitArray(other.Length + n);

            // actual shifting process
            for (var i = 0; i < other.Length; i++)
            {

                ans[i] = other[i];

            }


            return ans;

        }

/// <summary>
/// 异或
/// </summary>
/// <param name="one"></param>
/// <param name="two"></param>
/// <returns></returns>
        public static BitArray operator ^(BitArray one, BitArray two)
        {
            var sequence1 = one.ToString();
            var sequence2 = two.ToString();
            var result = "";
            var tmp = "";

            // for scaling of same length.
            if (one.Length != two.Length)
            {
                int difference;
                if (one.Length > two.Length)
                { // one is greater

                    difference = one.Length - two.Length;

                    // fills up with 0's
                    for (var i = 0; i < difference; i++)
                    {
                        tmp += '0';
                    }

                    tmp += two.ToString();
                    sequence2 = tmp;

                }
                else
                { // two is greater

                    difference = two.Length - one.Length;

                    // fills up with 0's
                    for (var i = 0; i < difference; i++)
                    {
                        tmp += '0';
                    }

                    tmp += one.ToString();
                    sequence1 = tmp;


                }

            } // end scaling

            var ans = new BitArray(one.Length);

            for (var i = 0; i < one.Length; i++)
            {
                if (sequence1[i] == sequence2[i])
                {
                    result += '0';
                }
                else
                {
                    result += '1';
                }
                //switch (sequence1[i])
                //{
                //    case '0':
                //        if (sequence2[i] == '1')
                //        {
                //            result += '1';
                //        }
                //        else
                //        {
                //            result += '0';
                //        }
                //        break;
                //    case '1':
                //        if (sequence2[i] == '0')
                //        {
                //            result += '1';
                //        }
                //        else
                //        {
                //            result += '0';
                //        }
                //        break;
                //}

            }

            result = result.Trim();
            ans.Compile(result);


            return ans;
        }

/// <summary>
/// 右移
/// </summary>
/// <param name="other"></param>
/// <param name="n"></param>
/// <returns></returns>
        public static BitArray operator >>(BitArray other, int n)
        {

            var ans = new BitArray(other.Length - n);

            // actual shifting process.
            for (var i = 0; i < other.Length - n; i++)
            {

                ans[i] = other[i];
            }

            return ans;

        }
/// <summary>
/// 等于
/// </summary>
/// <param name="one"></param>
/// <param name="two"></param>
/// <returns></returns>
        public static bool operator ==(BitArray one, BitArray two)
        {
            var status = true;

            if (one.Length == two.Length)
            {

                for (var i = 0; i < one.Length; i++)
                {

                    if (one[i] != two[i])
                    {
                        status = false;
                    }

                }

            }
            else
            {

                throw new Exception("== : inputs haven't same length!");

            }

            return status;

        }

        /**
		 * Operator != (not-equal)
		 * input: two bit-arrays
		 * output: returns true if there inputs aren't equal otherwise false.
		 * assumes: the input bit-arrays must have same length.
		 * */
        public static bool operator !=(BitArray one, BitArray two) => !(one == two);

        /**
		 * Indexer
		 * for selecting the individual bits.
		 * */
        public bool this[int offset]
        {
            get => _field[offset];
            set => _field[offset] = value;
        }

        /*
		 * NumberOfOneBits
		 * input: none
		 * output: the number of one-bits in the field. 
		 * */
        public int NumberOfOneBits()
        {
            var counter = 0;

            // counting one-bits.
            foreach (var bit in _field)
            {
                if (bit)
                {
                    counter++;
                }
            }
            return counter;
        }

        /*
		 * NumberOfZeroBits
		 * input: none
		 * output: the number of one-bits in the field. 
		 * */
        public int NumberOfZeroBits()
        {
            var counter = 0;

            // counting zero-bits
            foreach (var bit in _field)
            {
                if (!bit)
                {
                    counter++;
                }
            }
            return counter;
        }

        /**
		 * EvenParity
		 * input: none
		 * output: returns true if parity is even, otherwise false.
		 * */
        public bool EvenParity() => NumberOfOneBits() % 2 == 0;

        /**
		 * OddParity
		 * input: none
		 * output: returns true if parity is odd, otherwise false.
		 * */
        public bool OddParity() => NumberOfOneBits() % 2 != 0;

        /**
		 * ToInt64
		 * input: none
		 * output: returns a long integer representation of the bit-array.
		 * assumes: the bit-array length must been smaller or equal to 64 bit. 
		 * */
        public long ToInt64()
        {
            // Precondition
            if (_field.Length > 64)
            {
                throw new Exception("ToInt: field is too long.");
            }

            var sequence = ToString();
            return Convert.ToInt64(sequence, 2);
        }

        /**
		 * ToInt32
		 * input: none
		 * output: returns a integer representation of the bit-array.
		 * assumes: the bit-array length must been smaller or equal to 32 bit. 
		 * */
        public int ToInt32()
        {
            // Precondition
            if (_field.Length > 32)
            {
                throw new Exception("ToInt: field is too long.");
            }

            var sequence = ToString();
            return Convert.ToInt32(sequence, 2);
        }

/// <summary>
/// 重置bitArray的值
/// </summary>
        public void ResetField()
        {
            for (var i = 0; i < _field.Length; i++)
            {
                _field[i] = false;
            }
        }


        /// <summary>
        /// 重置bitArray的值
        /// </summary>
        /// <param name="flag">位值</param>
        public void SetAll(bool flag)
        {
            for (var i = 0; i < _field.Length; i++)
            {
                _field[i] = flag;
            }
        }


        /**
		 * CompareTo (interfaces IComparable)
		 * input: BitArray
		 * output: 0 - if the bit-array a equal.
		 * 		   -1 - if this bit-array is smaller.
		 * 			1 - if this bit-array is greater.
		 * assumes: bit-array lentgh must been smaller or equal to 64 bit
		 * */
        public int CompareTo(object other)
        {

            var status = 0;
            var valueThis = ToInt64();
            var otherBitArray = (BitArray)other;
            var valueOther = otherBitArray.ToInt64();

            if (valueThis > valueOther)
            {
                status = 1;
            }
            else if (valueOther > valueThis)
            {
                status = -1;
            }

            return status;
        }


        /**
		 * Equals
		 * input: BitArray
		 * output: returns true if there inputs are equal otherwise false.
		 * assumes: the input bit-arrays must have same length.
		 * */
        public override bool Equals(object other)
        {
            var status = true;

            var otherBitArray = (BitArray)other;

            if (Length == otherBitArray.Length)
            {

                for (var i = 0; i < Length; i++)
                {
                    if (_field[i] != otherBitArray[i])
                    {
                        status = false;
                    }
                }
            }
            else
            {
                throw new Exception("== : inputs haven't same length!");
            }

            return status;
        }

        /**
		 * GetHashCode
		 * input: none
		 * output: hash-code for this BitArray instanz.
		 * assumes: bit-array lentgh must been smaller or equal to 32
		 * */
        public override int GetHashCode() => ToInt32();

        /**
		 * Clone (interface ICloneable)
		 * input: none
		 * output: a copy of this bit-array
		 * */
        public object Clone()
        {
            var theClone = new BitArray(Length);

            for (var i = 0; i < Length; i++)
            {
                theClone[i] = _field[i];
            }

            return theClone;
        }

        /**
		 * Property (for interface IEnumerator)
		 * returns the current bit of the bit-field.
		 * */
        public object Current
        {
            get
            {
                try
                {
                    return _field[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        /**
		 * MoveNext (for interface IEnumerator)
		 * input: none
		 * output: returns true if 'position' successful increased otherwise false.
		 * */
        public bool MoveNext()
        {
            if (position + 1 < _field.Length)
            {
                position++;
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
		 * Reset (for interface IEnumerator)
		 * 	resets the position of the enumerator.
		 * */
        public void Reset() => position = -1;


        /**
		 * GetEnumerator (for interface IEnumerable)
		 * input: none
		 * output: returns a enumerator for this BitArray-Object.
		 * */
        public IEnumerator GetEnumerator() => this;

        /***
		 * Utility method
		 * input: string sequence
		 * output: returns true if sequence contains only zeros and ones,
		 * 			otherwise false.
		 * purpose: checks a given sequence contains only zeros and ones.
		 * 			This method will used in Constructor (sequence : string) 
		 * 			and Compile(sequence : string) 
		 * 
		 **/
         /// <summary>
         /// 将字符串转换为bool数组，char为0或1，对应的bool为true，否则为false
         /// </summary>
         /// <param name="sequence"></param>
         /// <returns></returns>
        private bool Match(string sequence)
        {
            var status = true;

            foreach (var ch in sequence)
            {
                if (ch != '0' && ch != '1')
                {
                    status = false;
                }

            }

            return status;
        }
    }
}
