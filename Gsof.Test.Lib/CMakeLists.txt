# CMakeList.txt: Gsof.Test.Lib 的 CMake 项目，在此处包括源代码并定义
# 项目特定的逻辑。
#
cmake_minimum_required(VERSION 3.22)

project (Gsof.Test.Lib)

# 将源代码添加到此项目的可执行文件。
# add_executable (Gsof.Test.Lib "Gsof.Test.Lib.c" "Gsof.Test.Lib.h")
add_library(Gsof.Test.Lib SHARED "Gsof.Test.Lib.c")


# TODO: 如有需要，请添加测试并安装目标。
