﻿#include "StringX.h"

#define TERMINAL 1 // Единица для подсчёта длины массива с учётом терминального нуля
#define NEW_ELEMENT 1 // Единица для добавления нового элемента
#define ITERATION_ISSUE 1 // Лишняя итерация в методах insert() и compress()

#define NEGATIVE -1 // Отрицательный результат для метода contains()




#pragma region NEW_METHODS


void StringX::compress()
{
	int size = getLength(); // Начальный размер строки

	int nSpaceCounter = 0; // Общее кол-во пробелов

	int nWordsCounter = 0; // Кол-во слов

	int nonSpace = 0; // Счётчик символов, не равных пробелам

	int nVanguard = 0; // Счётчик пробелов в начале строки


// Подсчёт слов, пробелов, обычных символов и пр.
#pragma region PREPARATIONS


	while (ptrArray[nVanguard++] == ' ') // Подсчёт пробелов в начале слова
	{
		
	}

	for (size_t i = 0ULL; i < size; i++) // Подсчёт пробелов во всей строке
	{
		if (' ' == ptrArray[i]) nSpaceCounter += 1;
		else nonSpace+=1;
	}

	if (size == nSpaceCounter) return; // Выход в случае, если в строке одни пробелы
	else
	{
		for (size_t i = 1ULL; i < size; i++) // Подсчёт слов во всей строке
		{
			if (' ' == ptrArray[i - 1] && ' ' != ptrArray[i]) nWordsCounter += 1;
		}
	}


#pragma endregion PREPARATIONS



// Копирование содержимого в буфер
#pragma region COPY

	int nCurrentWordsCounter = 0; // Счётчик слов в цикле копирования

	int newSize = nVanguard + nWordsCounter + nonSpace; // Новый размер массива

	char* buf = new char[newSize];

	for (size_t i = 0ULL, j = 0ULL; i < size && j < newSize;) // Цикл идёт до конца строки; одна итерация -- одно слово (по идее)
	{
		if (nCurrentWordsCounter++ == nWordsCounter) break; // Если все слова перезаписаны, то выход

		while (i < nVanguard) // Соранение начальных пробелов
		{
			buf[j++] = ptrArray[i++];
		}

		while (' ' != ptrArray[i] && i < size) // Копирование символов слов
		{
			buf[j++] = ptrArray[i++];
		}

		if (' ' == ptrArray[i]) // Один пробел между словами
		{
			buf[j++] = ' ';
			i++;
		}

		while (' ' == ptrArray[i]) // Пропуск дополнительных пробелов между словами
		{
			i++;
		}
	}

#pragma endregion COPY

	newSize = newSize - ITERATION_ISSUE - ITERATION_ISSUE; // Судя по всему, два цикла while у счётчиков делают лишнюю итерацию
	length = newSize;
	buf[newSize] = '\0';
	delete[]this->ptrArray; // Раньше здесь случался RuntimeError, если в конце строки нет пробелов
	this->ptrArray = buf;

}


StringX StringX::embrace(StringX& first, StringX second)
{
	if (-1 != first.contains(second.getArrayPtr()))
	{
		int containsCounter = 0;
		int matchCounter = second.size();
		int tempCounter;

		int c = 0;

		for (size_t i = 0ULL; i < first.size(); i++) // Счтает кол-во вхождений
		{
			tempCounter = 0;
			if (first.getArrayPtr()[i] == second.getArrayPtr()[0])
			{
				c = i;
				for (size_t j = 0ULL, v = i; j < second.size(); j++)
				{
					if (first.getArrayPtr()[c] == second.getArrayPtr()[j])
					{
						tempCounter += 1;
						c+=1;
					}
				}
				if (tempCounter == second.size()) containsCounter += 1;
			}
		}


		int* containsIndexes = new int[containsCounter];

		for (size_t i = 0ULL, k = 0ULL; i < first.size(); i++) // Записываем в массив координаты вхождений
		{
			tempCounter = 0;
			if (first.getArrayPtr()[i] == second.getArrayPtr()[0])
			{
				c = i;
				for (size_t j = 0ULL, v = i; j < second.size(); j++)
				{
					if (first.getArrayPtr()[c] == second.getArrayPtr()[j])
					{
						tempCounter += 1;
						c += 1;
					}
				}
				if (tempCounter == second.size())
				{
					containsIndexes[k] = i;
					k++;
				}
			}
		}

		int nBracersQuantity = 0;

		StringX newFirst;

		for (size_t i = 0ULL; i < first.length;) // Копирование с расстановками скобок в нужных местах
		{
			for (size_t j = 0ULL; j < containsCounter; j++)
			{
				if (i == containsIndexes[j])
				{
					newFirst.addSymbol('(');

					for (size_t k = 0; k < second.length; k++)
					{
						newFirst.addSymbol(second.getArrayPtr()[k]);
						i+=1;
					}

					newFirst.addSymbol(')');
				}

				else
				{
					newFirst.addSymbol(first.getArrayPtr()[i]);
					i += 1;
				}
			}
		}

		first.clear();

		for (size_t i = 0ULL; i < newFirst.getLength(); i++) // копирование строки в первую строку, согласно условию задачи
		{
			first.addSymbol(newFirst.getArrayPtr()[i]);
		}

		return first;
		
	}
	else return first;
}


int StringX::size()
{
	return length;
}


bool StringX::isEmpty()
{
	if (0 == length) return true; // NOT "<= 0" !!!!!!!!!!!!
	return false;

	//
	// Okay, I'll try and write this prescription for future me.
	// So, in That line I've cought a bug, that I was searching for about 3 days.
	// I believe that at some point compiler assignes a MIN_INT value to nLength,
	// and passes it forth. And if you put "<=" instead of "==", it will cause a chain reaction.
	// Thus "isEmpty" method would not work, then "<<" and so on.
	// I really haven't got it, may be I should study this "thing" and then
	// ask our teacher if it won't clear. Ridiculous.
	// 
}


void StringX::clear()
{
	if (nullptr != ptrArray)
	{
		delete[]ptrArray;
		ptrArray = nullptr;
		length = 0;
	}
	else length = 0;
}


#pragma endregion NEW_METHODS




#pragma region OVERLOADING_OPERATORS


// res 0 - не равны, res 1 - общий размер, res 10 - содержимое идентично
int StringX::operator == (StringX& right)
{
	int res;
	if (right.size() != size())
	{
		res = 0;
		return res;
	}
	else
	{
		res = 1;
		int counter = 0;
		for (int i = 0; i < size(); i++)
		{
			if (ptrArray[i] == right.getArrayPtr()[i]) counter += 1;
		}
		if (size() != counter) return res;
		else
		{
			res = 10;
			return res;
		}
	}
}


char& StringX::operator[](const int& index) const
{
	return ptrArray[index];
}


StringX& StringX::operator=(StringX& right)
{
	clear();

	if (!right.isEmpty())
	{
		int size = 0;
		size += TERMINAL + right.length;
		if (nullptr != ptrArray) delete[]ptrArray;
		ptrArray = new char[size];
		size -= TERMINAL;

		length = size;

		for (int i = 0; i < size; i++)
		{
			ptrArray[i] = right.getArrayPtr()[i];
		}
	}

	return *this;
}


StringX& StringX::operator=(const char* right)
{
	clear();

	int size = TERMINAL;
	for (int i = 0; right[i] != '\0'; i++)
	{
		size += 1;
	}

	if (1 != size)
	{
		ptrArray = new char[size];
		size -= TERMINAL;

		length = size;

		for (int i = 0; i < size; i++)
		{
			ptrArray[i] = right[i];
		}
	}

	return *this;
}


std::ostream& operator<<(std::ostream& outputStream, StringX& right)
{
	if (!right.isEmpty())
	{
		for (int i = 0; i < right.size(); i++)
		{
			outputStream << right.getArrayPtr()[i];
		}
	}
	else
	{
		outputStream << "String is Empty!";
	}
	return outputStream;
}


void StringX::operator + (StringX& right)
{
	return merge(right);
}


StringX* StringX::operator / (const char& right)
{
	return split(right);
}


int StringX::operator % (const char* right)
{
	return contains(right);
}


#pragma endregion OVERLOADING_OPERATORS




#pragma region LOGIC




void StringX::addSymbol(const char cSymbol)
{
	char* buf = new char[length + NEW_ELEMENT + TERMINAL];

	for (int i = 0; i < length; i++)
	{
		buf[i] = ptrArray[i];
	}

	buf[length] = cSymbol;

	buf[length + TERMINAL] = '\0';

	length += NEW_ELEMENT;

	delete[]ptrArray;

	ptrArray = buf;

}


int StringX::contains(const char* sSearchedString)
{
	int nReturnValue = NEGATIVE;

	int nSearchedSize = 0;

	int counter = 0;

	for (int i = 0; sSearchedString[i] != '\0'; i++)
	{
		nSearchedSize += 1;
	}

	if (nSearchedSize > getLength()) return -1;

	for (int i = 0; i < length; i++)
	{
		counter = 0;

		if (ptrArray[i] == sSearchedString[0])
		{
			nReturnValue = i;

			for (int j = 0, k = i; j < nSearchedSize && k < length; j++, k++)
			{
				if (ptrArray[k] == sSearchedString[j]) counter++;
			}

			if (nSearchedSize == counter) return nReturnValue;
			else nReturnValue = NEGATIVE;
		}
	}

	return nReturnValue;
}


void StringX::insert(const char* sInsertedString, int nPosition)
{
	int sizeIns = 0, sizeAll = TERMINAL, tempIterator = 0;

	for (int i = 0; sInsertedString[i] != '\0'; i++)
	{
		sizeIns += 1;
	}

	sizeAll += length + sizeIns;

	char* buf = new char[sizeAll];

	sizeAll -= TERMINAL;

	buf[sizeAll] = '\0';

	for (int i = 0, j = 0; i < sizeAll; i++)
	{
		if (i != nPosition)
		{
			buf[i] = ptrArray[j++];
		}
		else
		{
			tempIterator = i;

			for (int k = 0; k < sizeIns; k++)
			{
				buf[tempIterator++] = sInsertedString[k];
			}

			tempIterator -= i;

			i += tempIterator - ITERATION_ISSUE;
		}
	}

	length = sizeAll;

	delete[]ptrArray;
	ptrArray = buf;
}


void StringX::insert(StringX sInsertedString, int nPosition)
{
	int sizeIns = 0, sizeAll = TERMINAL, tempIterator = 0;

	const char* sInsertedStringPtr = sInsertedString.getArrayPtr();

	for (int i = 0; sInsertedStringPtr[i] != '\0'; i++)
	{
		sizeIns += 1;
	}

	sizeAll += length + sizeIns;

	char* buf = new char[sizeAll];

	sizeAll -= TERMINAL;

	buf[sizeAll] = '\0';

	for (int i = 0, j = 0; i < sizeAll; i++)
	{
		if (i != nPosition)
		{
			buf[i] = ptrArray[j++];
		}
		else
		{
			tempIterator = i;

			for (int k = 0; k < sizeIns; k++)
			{
				buf[tempIterator++] = sInsertedStringPtr[k];
			}

			tempIterator -= i;

			i += tempIterator - ITERATION_ISSUE;
		}
	}

	length = sizeAll;

	delete[]ptrArray;
	ptrArray = buf;
}


void StringX::merge(StringX secondString)
{
	int sizeFirst = getLength();
	int sizeSecond = secondString.getLength();
	int sizeAll = sizeFirst + sizeSecond + TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	char* buf = new char[sizeAll];

	sizeAll -= TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	for (int i = 0; i < sizeFirst && i < sizeAll; i++)
	{
		buf[i] = ptrArray[i];
	}

	for (int i = 0, j = sizeFirst; i < sizeSecond && j < sizeAll; i++, j++)
	{
		buf[j] = secondString.getArrayPtr()[i];
	}

	length = sizeAll;
	buf[sizeAll] = '\0';

	delete[] ptrArray;
	ptrArray = buf;
}


StringX* StringX::split(char separator)
{
	const int size = length;

	StringX* buf = new StringX[1];

	buf[0].setString("false"); // В случае неудачи, вернём эту же строку

	int counter = 0; // Счётчик символов в строке

	for (int i = 0; i < size; i++)
	{
		if (separator == ptrArray[i]) counter++;
	}

	if (0 != counter)
	{
		counter += 1; // Строк всегда будет на 1 больше, чем символов

		delete[]buf; // 

		buf = new StringX[counter];

		int j = 0;

		for (int i = 0; i < counter && j < size; i++, j++)
		{
			while (ptrArray[j] != separator)
			{
				if (size == j) break;
				buf[i].addSymbol(ptrArray[j]);
				j += 1;
			}
		}

	}
	return buf;
}




#pragma endregion LOGIC




#pragma region CONSTRUCT


StringX::StringX()
{
	ptrArray = nullptr;
	length = 0;
}


StringX::StringX(const char* Array)
{
	int size = TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	for (int i = 0; Array[i] != '\0'; i++)
	{
		size += 1;
	}

	ptrArray = new char[size];

	size -= TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	length = size;

	for (int i = 0; i < size; i++)
	{
		ptrArray[i] = Array[i];
	}

	ptrArray[size] = '\0';
}


StringX::StringX(StringX& copyString)
{
	int size = copyString.length + TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	ptrArray = new char[size];

	size -= TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	for (int i = 0; i < size; i++)
	{
		ptrArray[i] = copyString.ptrArray[i];
	}

	length = size;

	ptrArray[size] = '\0';
}


StringX::~StringX()
{
	if (nullptr != ptrArray) delete[]ptrArray;
}


#pragma endregion CONSTRUCT




#pragma region ACCESS


int StringX::getLength()
{
	return this->length;
}


void StringX::setString(const char* newString)
{
	int size = TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	for (int i = 0; newString[i] != '\0'; i++)
	{
		size += 1;
	}

	ptrArray = new char[size];

	size -= TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	length = size;

	for (int i = 0; i < size; i++)
	{
		ptrArray[i] = newString[i];
	}

	ptrArray[size] = '\0';
}


const char* StringX::getArrayPtr()
{
	return this->ptrArray;
}


#pragma endregion ACCESS
