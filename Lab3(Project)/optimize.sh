#!/bin/bash
cd ~/selection_sort
ml icc
exectime=0

timeIt(){
	usr/bin/time -f "%U" ./SelectionSort$ind 2>&1
}

icc SelectionSort.cpp -o SelectionSort$ind
exectime=$(timeIt)
echo "Time without optimizations is $exectime seconds"

for i in {0..3}
do
	icc -O$i SelectionSort.cpp -o SelectionSort$ind
	exectime=$(timeIt)
	echo "Time with O$i is $exectime seconds"
done

extentions="sse2 sse3 ssse3 sse4.1 sse4.2 avx"
for ext in $extentions
do
	icc -x$ext SelectionSort.cpp -o SelectionSort$ind
	exectime=$(timeIt)
	echo "Time with extention $ext is $exectime seconds"
done
