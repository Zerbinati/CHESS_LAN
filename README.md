# CHESS LAN
Tool to play network matches between chess engines

Prerequisites :<br>
copy [engine.exe.exe](https://github.com/chris13300/CHESS_LAN/blob/main/CHESS%20LAN/bin/x64/Debug/engine.exe) to your CHESS LAN folder<br>

rename BUREAU.ini to YOUR_COMPUTER_NAME.ini<br>
set txtEngine to path_to_your_engine<p>

rename BrainFish.txt to NAME_OF_YOUR_ENGINE.txt<br>
set its UCI options<p>

# How it works ?
- Run CHESS LAN on one of your computers, set your settings, select Server, click on the "LISTEN" button :<br>
![server_listen](https://github.com/chris13300/CHESS_LAN/blob/main/CHESS%20LAN/bin/Debug/server_listen.jpg)<p>
  
- Run CHESS LAN on another computer, set your settings, select Client, click on the "CONNECT" button :<br>
![client_connect](https://github.com/chris13300/CHESS_LAN/blob/main/CHESS%20LAN/bin/Debug/client_connect.jpg)<p>

During the tourney, we get few files :<br>
- the "computer_name_reception.log" file contains the data received from the other computer<br>
- the "computer_name_transmission.log" file contains the data sent to the other computer<br>
- the "stats_computer_name_delay_increment_thread_hash_ponder.ini" file contains the statistics of the current tourney<br>
- the "check.pgn" file contains the games played during the tourney<p>

# tips
As the "client" player, you don't need to set Delay, Increment, Ponder, FEN.<br>
These settings will be sent by the "server" player.<p>
