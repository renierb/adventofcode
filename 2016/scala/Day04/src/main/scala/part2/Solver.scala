package part2

class Solver(lines: Stream[String]) {

  private val letters = ('a' to 'z').toArray

  case class RoomHash(name: Array[String], sectorID: Int, checksum: String) {
    def isReal: Boolean = {
      val mostCommon = name.flatten.groupBy(c => c).toSeq
        .sortBy(g => (-g._2.length, g._1))
        .map(_._1)
        .mkString

      mostCommon.startsWith(checksum)
    }

    lazy val realName: String = {
      name.map(_.map { c =>
        val index = letters.indexOf(c) + (sectorID % 26)
        letters(index % 26)
      }.mkString).mkString(" ")
    }
  }

  def solve(startsWith: String): RoomHash = {
    val hashes = lines.map { l =>
      val groups: Map[Int, Array[String]] = l.split(Array('-', '['))
        .groupBy { s =>
          if (s(0).isLetter && !s.endsWith("]"))
            0
          else if (s(0).isDigit)
            1
          else
            2
        }
      RoomHash(groups(0), groups(1).mkString.toInt, groups(2).mkString.init)
    }

    hashes.filter(p => p.isReal && p.realName.startsWith(startsWith)).head
  }
}
