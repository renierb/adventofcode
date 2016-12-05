package part2

trait DomainDef {

  import scala.collection.{IndexedSeq => $}
  val keypad = $(
    "---1---",
    "--234--",
    "-56789-",
    "--ABC--",
    "---D---"
  )

  val maxRow = keypad.length - 1
  val maxCol = keypad(0).length - 1

  lazy val keys: Map[Char, (Int, Int)] = keypad.zipWithIndex.flatMap {
    case (keys, i) => {
      keys.zipWithIndex.map {
        case (key, j) => key -> (i, j)
      }
    }
  }.toMap

  trait Move {
    def move(key: (Int, Int)): (Int, Int)
  }
  case object Up extends Move {
    override def move(key: (Int, Int)): (Int, Int) = {
      val (row, col) = key
      if (row == 0 || keypad(row - 1)(col) == '-')
        (row, col)
      else
        (row - 1, col)
    }
  }
  case object Down extends Move {
    override def move(key: (Int, Int)): (Int, Int) = {
      val (row, col) = key
      if (row == maxRow || keypad(row + 1)(col) == '-')
        (row, col)
      else
        (row + 1, col)
    }
  }
  case object Left extends Move {
    override def move(key: (Int, Int)): (Int, Int) = {
      val (row, col) = key
      if (col == 0 || keypad(row)(col - 1) == '-')
        (row, col)
      else
        (row, col - 1)
    }
  }
  case object Right extends Move {
    override def move(key: (Int, Int)): (Int, Int) = {
      val (row, col) = key
      if (col == maxCol || keypad(row)(col + 1) == '-')
        (row, col)
      else
        (row, col + 1)
    }
  }

  case class CodeInterpreter(key: Char) {

    def interpret(instructions: String): Char = {
      val (row, col) = instructions.foldLeft(keys(key)) { (k, i) =>
        i match {
          case 'U' => Up.move(k)
          case 'D' => Down.move(k)
          case 'L' => Left.move(k)
          case 'R' => Right.move(k)
        }
      }
      keypad(row)(col)
    }
  }
}
