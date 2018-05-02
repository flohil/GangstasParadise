import csv
import glob
import os.path

csv_paths = "data/top25/*.csv"
txt_path = "data/top25.txt"
filter_chars = set('()[]:+*>')

if os.path.isfile(txt_path):
    os.remove(txt_path)

for csv_name in glob.glob(csv_paths):
    print("reading in: ", csv_name)

    with open(csv_name, errors="replace") as csv_file, open(txt_path, 'a') as txt_file:
        reader = csv.reader(csv_file, delimiter=',', quotechar='"')

        # skip header line
        next(reader)

        row = None

        for row in reader:
            if (len(row) > 6):
                lyrics = row[6]
                lyric_lines = lyrics.splitlines()

                for line in lyric_lines:

                    # filter out lines that do not contain actual prosa, eg. "(Eminem + Z-Dog)" or "Verse 1:"
                    if not any((c in filter_chars) for c in line):
                        print(line)
                        txt_file.write(line.replace('"', '') + '\n')