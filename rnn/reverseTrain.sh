#! /bin/bash

for file in `ls data`; do
  python reverse.py --data_dir=data/$file --save_dir=reversed/$file --num_layers=3 --seq_length=75 --rnn_size=512 --num_epochs=60
  echo "Trained for $file";
done
