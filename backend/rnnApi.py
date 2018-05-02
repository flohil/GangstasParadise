import naiveSample
import schemeSample

class Args:
    def __init__(self, prime, lines, sample, forward_dir, reversed_dir, post_dir):
        self.prime = prime
        self.n = lines
        self.sample = sample
        self.forward_dir = forward_dir
        self.reversed_dir = reversed_dir
        self.post_dir = post_dir

def talk_about_yourself(prime="I ", lines=100):
    args = Args(prime, lines, 2, "save/top25_3", "reversed/top25_3", "post/top25_3")

    return naiveSample.sample(args)


def rap(prime=" ", lines=100):
    args = Args(prime, lines, 2, "save/top25_3", "reversed/top25_3", "post/top25_3")

    return schemeSample.sample(args)