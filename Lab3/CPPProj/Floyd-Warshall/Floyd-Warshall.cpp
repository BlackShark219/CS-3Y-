#include <iostream>
#include <cstdlib>
using namespace std;
void floyds(int b[][300], int a)
{
	int i, j, k;
	for (k = 0; k < a; k++)
	{
		for (i = 0; i < a; i++)
		{
			for (j = 0; j < a; j++)
			{
				if ((b[i][k] * b[k][j] != 0) && (i != j))
				{
					if ((b[i][k] + b[k][j] < b[i][j]) || (b[i][j] == 0))
					{
						b[i][j] = b[i][k] + b[k][j];
					}
				}
			}
		}
	}
}
int main()
{
	int a = 300;
	int b[300][300];
	for (int r = 0; r < 100; r++)
	{
		for (int i = 0; i < a; i++)
		{
			for (int j = 0; j < a; j++)
			{
				b[i][j] = rand();
			}
		}
		floyds(b, a);
	}
}