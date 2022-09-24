set(LIB_NAME DummyLib)

project(${LIB_NAME})

aux_source_directory(. SRC_FILES)

add_library(${LIB_NAME} STATIC ${SRC_FILES})

set(PUBLIC_LIBRARY_HEADERS dummy.hpp)

install(FILES ${PUBLIC_LIBRARY_HEADERS} DESTINATION include)

install()