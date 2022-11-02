for i in $(seq -f "%02g" 0 9)
do
  touch "$1"/file"$i".txt
done
