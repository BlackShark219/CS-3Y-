#!/bin/bash
exectime=$(usr/bin/time -f "%U" ./$file 2>&1)
echo "Time with $ext: $exectime"