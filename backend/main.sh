
echo "Training...."
python post_train.py --data_dir=data/top25 --save_dir=post/top25_3 --rnn_size=512 --num_layers=3 --seq_length=100 --batch_size=100 --num_epochs=23 --init_from=post/top25_3
python train.py --data_dir=data/top25 --save_dir=save/top25_3 --rnn_size=512 --num_layers=3 --seq_length=100 --batch_size=100 --num_epochs=23 --init_from=save/top25_3
python reverse.py --data_dir=data/top25 --save_dir=reversed/top25_3 --rnn_size=512 --num_layers=3 --seq_length=100 --batch_size=100 --num_epochs=23 --init_from=reversed/top25_3
echo "Training State saves to post.log, forward.log, and reverse.log"

echo "Naive Sampling..."
python naiveSample.py --forward_dir=save/top25_3 --reversed_dir=reversed/top25_3 --post_dir=post/top25_3 --sample=2 -n 1000 > finalSamples/Naiive/top25_3.txt

echo "Scheme Sampling..."
python schemeSample.py --forward_dir=save/top25_3 --reversed_dir=reversed/top25_3 --post_dir=post/top25_3 --sample=2 -n 1000 > finalSamples/Scheme/top25_3.txt