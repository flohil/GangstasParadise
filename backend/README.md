rapRNN
===

Based on [rhyme-rnn-tensorflow](https://github.com/jayIves/rhyme-rnn-tensorflow).

## Requirements
- pip install tensorflow-gpu
- pip install numpy
- pip install six

## Basic Usage

python naiveSample.py --forward_dir=save/top25_3 --reversed_dir=reversed/top25_3 --post_dir=post/top25_3 --sample=2 -n 100

python schemeSample.py --forward_dir=save/top25_3 --reversed_dir=reversed/top25_3 --post_dir=post/top25_3 --sample=2 -n 100
