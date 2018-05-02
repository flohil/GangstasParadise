rhyme-rnn-tensorflow
===

[![Join the chat at https://gitter.im/char-rnn-tensorflow/Lobby](https://badges.gitter.im/char-rnn-tensorflow/Lobby.svg)](https://gitter.im/char-rnn-tensorflow/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Coverage Status](https://coveralls.io/repos/github/sherjilozair/char-rnn-tensorflow/badge.svg)](https://coveralls.io/github/sherjilozair/char-rnn-tensorflow)
[![Build Status](https://travis-ci.org/sherjilozair/char-rnn-tensorflow.svg?branch=master)](https://travis-ci.org/sherjilozair/char-rnn-tensorflow)

Multi-layer Recurrent Neural Networks (LSTM, RNN) for character-level language models in Python using Tensorflow.

Inspired from Andrej Karpathy's [char-rnn](https://github.com/karpathy/char-rnn).

## Requirements
- [Tensorflow 1.0](http://www.tensorflow.org)

## Basic Usage
You can run this module with ./main.sh

## Datasets
You can use any plain text file as input. For example you could download [The complete Sherlock Holmes](https://sherlock-holm.es/ascii/) as such:

```bash
cd data
mkdir sherlock
cd sherlock
wget https://sherlock-holm.es/stories/plain-text/cnus.txt
mv cnus.txt input.txt
```

Then start train from the top level directory using `python train.py --data_dir=./data/sherlock/`

A quick tip to concatenate many small disparate `.txt` files into one large training file: `ls *.txt | xargs -L 1 cat >> input.txt`

## Tensorboard
To visualize training progress, model graphs, and internal state histograms:  fire up Tensorboard and point it at your `log_dir`.  E.g.:
```bash
$ tensorboard --logdir=./logs/
```

Then open a browser to [http://localhost:6006](http://localhost:6006) or the correct IP/Port specified.

## Contributing
Please feel free to:
* Leave feedback in the issues
* Open a Pull Request
* Join the [gittr chat](https://gitter.im/char-rnn-tensorflow/Lobby)
* Share your success stories and data sets!
