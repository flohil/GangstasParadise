from __future__ import print_function
import tensorflow as tf

import argparse
import os
from six.moves import cPickle

from model import LineModel,ReversedModel,PostModel

from six import text_type


def main():
    parser = argparse.ArgumentParser(
                       formatter_class=argparse.ArgumentDefaultsHelpFormatter)
    parser.add_argument('--forward_dir', type=str, default='save',
                        help='model directory to store forward checkpointed models')
    parser.add_argument('--reversed_dir', type=str, default='reversed',
                        help='model directory to store reversed checkpointed models')
    parser.add_argument('--post_dir', type=str, default='reversed',
                        help='model directory to store post checkpointed models')
    parser.add_argument('-n', type=int, default=5,
                        help='number of characters to sample')
    parser.add_argument('--prime', type=text_type, default=u' ',
                        help='prime text')
    parser.add_argument('--sample', type=int, default=1,
                        help='0 to use max at each timestep, 1 to sample at '
                             'each timestep, 2 to sample on spaces')

    args = parser.parse_args()
    sample(args)


def sample(args):
    forward_args, reversed_args,post_args = None,None, None
    forward_chars, forward_vocab = None, None
    reversed_chars, reversed_vocab = None, None
    post_chars, post_vocab = None, None

    # Load arguments
    with open(os.path.join(args.forward_dir, 'config.pkl'), 'rb') as f:
        forward_args = cPickle.load(f)
    with open(os.path.join(args.reversed_dir, 'config.pkl'), 'rb') as f:
        reversed_args = cPickle.load(f)
    with open(os.path.join(args.post_dir, 'config.pkl'), 'rb') as f:
        post_args = cPickle.load(f)
    
    # Load vocabularies
    with open(os.path.join(args.forward_dir, 'chars_vocab.pkl'), 'rb') as f:
        forward_chars, forward_vocab = cPickle.load(f)
    with open(os.path.join(args.reversed_dir, 'chars_vocab.pkl'), 'rb') as f:
        reversed_chars, reversed_vocab = cPickle.load(f)
    with open(os.path.join(args.forward_dir, 'chars_vocab.pkl'), 'rb') as f:
        forward_chars, forward_vocab = cPickle.load(f)

    model = LineModel(forward_args, training=False)
    print("Hit")
    forwardPass = None
    backwardsPass = None
    #postModel = PostModel(saved_args, training=False)
    with tf.Session() as sess:
        tf.global_variables_initializer().run()
        saver = tf.train.Saver(tf.global_variables())
        ckpt = tf.train.get_checkpoint_state(args.forward_dir)
        if ckpt and ckpt.model_checkpoint_path:
            saver.restore(sess, ckpt.model_checkpoint_path)
            forwardPass = model.sample(sess, forward_chars, forward_vocab, args.n, args.prime,
                               args.sample)
    print(forwardPass)

    tf.reset_default_graph()
    reversedModel = ReversedModel(reversed_args, training=False)

    with tf.Session() as sess:
        tf.global_variables_initializer().run()
        saver = tf.train.Saver(tf.global_variables())
        ckpt = tf.train.get_checkpoint_state(args.reversed_dir)
        if ckpt and ckpt.model_checkpoint_path:
            saver.restore(sess, ckpt.model_checkpoint_path)
            backwardsPass = reversedModel.sample(sess, reversed_chars, reversed_vocab, args.n, "\nnwa",
                               args.sample)
    print()
    print(backwardsPass)
if __name__ == '__main__':
    main()
