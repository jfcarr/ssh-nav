RELEASE_DIR = releases/

default:
	@echo 'Targets:'
	@echo '  publish-linux   : Build a self-contained release binary for Linux'
	@echo '  publish-windows : Build a self-contained release binary for Windows'
	@echo '  deploy-linux    : Copy release binary and app config to ~/bin'
	@echo '  release         : Build all binaries and copy with configuration'
	@echo '  clean           : dotnet clean'

publish-linux:
	dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true

publish-windows:
	dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

deploy-linux: publish-linux
	cp bin/Release/net10.0/linux-x64/publish/ssh-nav ~/bin
	cp ssh-nav.json.live ~/bin/ssh-nav.json

release: publish-linux publish-windows
	mkdir -p $(RELEASE_DIR)
	rm -f $(RELEASE_DIR)*
	cp bin/Release/net10.0/linux-x64/publish/ssh-nav $(RELEASE_DIR)
	cp bin/Release/net10.0/win-x64/publish/ssh-nav.exe $(RELEASE_DIR)
	cp ssh-nav.json $(RELEASE_DIR)
	cd $(RELEASE_DIR); tar -czf ssh-nav-linux.tar.gz ssh-nav ssh-nav.json
	cd $(RELEASE_DIR); zip ssh-nav-windows.zip ssh-nav.exe ssh-nav.json

clean:
	dotnet clean
