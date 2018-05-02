#! /bin/bash

for file in `ls data`; do
  python post_train.py --data_dir=data/$file --save_dir=post/$file --num_layers=3 --seq_length=9 --rnn_size=256 --num_epochs=100
  echo "Trained for $file";
done
