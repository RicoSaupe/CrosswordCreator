module src.Models

type BoardCoord = int * int
type Word = string
type Words = List<Word>
type WordCount = int

type LayoutDir =
    | Horizontal
    | Vertical

type Cell = char
type Board = Cell [,]
type InputWord = { Word: Word; Hint: string }
type InputWords = InputWord list

type PuzzleWord =
    { Word: Word
      Hint: string
      Coord: BoardCoord
      Dir: LayoutDir
      Index: int }

type PuzzleWords = PuzzleWord list

type Puzzle = Board * WordCount * PuzzleWords

type Rank = int
type IntersectionPoint = BoardCoord * LayoutDir * Rank
