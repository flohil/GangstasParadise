#! /bin/bash

for file in `ls save/`; do 
  echo -e `python sample.py -n 1000 --save_dir save/$file` > samples/$file/sample.txt; 
done
