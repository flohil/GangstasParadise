import thecypher
import pandas as pd
import os.path

# list taken from https://tmthyjames.github.io/posts/Analyzing-Rap-Lyrics-Using-Word-Vectors/
artists = [
    'Tupac', 'Eminem', 'The Notorious B.I.G.', 'Nas', 'Ice Cube', 'Jay-Z', 'Snoop Dogg', 'Dr. Dre', 'Kendrick Lamar', 'Rakim', 'André 3000', 'Eazy-E', 'Kanye West', '50 Cent', 'DMX', 'Busta Rhymes', 'Method Man', 'J. Cole', 'Mos Def', 'Ludacris', 'KRS-One', 'LL Cool J', 'Lil Wayne', 'Common', 'Big L', 'Ghostface Killah', 'Redman', 'T.I.', 'Big Pun', 'Nate Dogg', 'Tech N9ne', 'Lauryn Hill', 'Scarface', 'Slick Rick', 'Raekwon', 'Big Daddy Kane', "Ol' Dirty Bastard", 'The Game', 'Mobb Deep', 'Logic', 'Chance the Rapper', 'Cypress Hill', 'Ice-T', 'Lupe Fiasco', 'RZA', 'GZA', 'Q-Tip', 'Warren G', 'Talib Kweli', 'Xzibit', 'Missy Elliott', 'ASAP Rocky', 'Immortal Technique', 'Twista', 'Big Sean', 'Kid Cudi', 'Big Boi', 'Chuck D', 'Donald Glover', 'Drake', 'Wiz Khalifa', 'Eric B. & Rakim', 'Schoolboy Q', 'DMC', 'Nelly', 'Hopsin', 'D12', 'Jadakiss', 'Tyler, the Creator', 'Kurupt', 'Grandmaster Flash and the Furious Five', 'Gang Starr', 'Too $hort', 'MC Ren', 'E-40', 'Pusha T', 'Coolio', 'De La Soul', 'Proof', 'Bad Meets Evil', 'Guru', 'Will Smith', 'Krayzie Bone', 'Black Thought', 'B.o.B', 'AZ', 'Yelawolf', 'The Sugarhill Gang', 'Earl Sweatshirt', 'Fabolous', 'Mac Miller', 'Fat Joe', 'Young Jeezy', 'Kool G Rap', 'Bizzy Bone', 'Queen Latifah', 'Prodigy', '2 Chainz'
]

iterator = 0

for artist in artists:
    filename = 'data/artists/' + str(iterator) + '.csv'

    if not os.path.isfile(filename):
        lyrics = []

        # our Cypher code
        artist_lyrics = thecypher.get_lyrics(artist)

        # append each record
        [lyrics.append(i) for i in artist_lyrics]

        print("finished downloading lyrics for artist " + artist)

        # convert to a DataFrame
        lyrics_df = pd.DataFrame(lyrics)

        group = ['song', 'year', 'album', 'genre', 'artist']
        lyrics_by_song = lyrics_df.sort_values(group).groupby(group).lyric.apply('\n'.join).reset_index(name='lyric')

        lyrics_by_song.to_csv(filename)

        print("exported songs to csv for artist " + artist)

    iterator += 1

