#! /bin/bash

for file in `ls save/`; do 
  echo -e `python post_sample.py -n 1000 --save_dir post/$file` > post_sample/$file/sample.txt; 
done
