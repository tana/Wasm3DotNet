(module
  ;; Written in WebAssembly Text Format (wat)
  ;;   see: https://developer.mozilla.org/en-US/docs/WebAssembly/Understanding_the_text_format
  ;; Please assemble with the following command:
  ;;   wat2wasm test.wat
  (import "externals" "print_add" (func $print_add (param i32 i32) (result i32)))
  (export "test" (func $test))
  (func $test (param $x i32) (result i32)
    local.get $x
    i32.const 1
    call $print_add
  )
)