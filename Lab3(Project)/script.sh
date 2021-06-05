#!/bin/bash
cd ~/selection_sort
ml icc

icc FindLowest.cpp -o fl_not_$ind
qsub -N fl_not_$ind -l nodes=1:ppn=1,walltime=00:30:00 -v file=fl_not_$ind ext=none timeit.sh

for i in {0..3}
do
	icc -O$i FindLowest.cpp -o fl_O$i,$ind
	qsub -N MyJob_O$i_$ind -l nodes=1:ppn=1,walltime=00:30:00 -v file=ss_O$i_$ind ext=O$i timeit.sh
	
done

extentions="sse2 sse3 ssse3 sse4.1 sse4.2 avx"
for ext in $extentions
do
	icc -x$ext FindLowest.cpp -o fl_$ex,$ind
	qsub -N MyJob_$ext,$ind -l nodes=1:ppn=1,walltime=00:30:00 -v file=ss_$ext_$ind ext=$ext timeit.sh
	
done
