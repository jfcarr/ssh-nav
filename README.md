# SSH Navigator

SSH Navigator is a convenience utility for managing SSH and SFTP sessions.

## Requirements

You'll need **ssh** and **sshpass**.

If you plan to build the project yourself, you'll need the **.NET 10 SDK**.

## Configuration

Configurations are maintained in the `HostDefinitions` section of the **ssh-nav.json** file.  The default .json file has a single example configuration:

```json
{
    "Name": "u50",
    "Type": "ssh",
    "HostName": "unix50.org",
    "UserName": "unix50",
    "Password": "unix50",
    "PasswordType": "inline",
    "Port": 22
}
```

You can add your own configurations by adding new entries in the `HostDefinitions` section that follow the same layout.  If you plan to use the Makefile to publish and deploy, first make a copy of the configuration file and name it **ssh-nav.json.live**, then add your configuration entries to that file.

## Common Tasks

View all available options:

```bash
ssh-nav --help
```

### Display Host Definitions

```bash
ssh-nav -l
```

Output:

```txt
┌───────────┬───────────┬────────────┬────────┬────────────┐
│ Name (-n) │ Type (-t) │ Host Name  │ User   │ Password   │
├───────────┼───────────┼────────────┼────────┼────────────┤
│ u50       │ ssh       │ unix50.org │ unix50 │ Hard Coded │
└───────────┴───────────┴────────────┴────────┴────────────┘
```

### Launch a SSH Session

```bash
ssh-nav -n u50 -t ssh
```

Output:

```txt
SDF Public Access UNIX System presents ...

   /~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/
   /~/~ H Y S T E R I C A L ~ U N I X ~ S Y S T E M S ~/~/
   /~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/~/

    [a]  UNICS (Version Zero)   PDP-7       Summer   1969
    [b]  First Edition UNIX     PDP-11/20   November 1971
    [c]  Fifth Edition UNIX     PDP-11/40   June     1974
    [d]  Sixth Edition UNIX     PDP-11/45   May      1975
    [e]  Seventh Edition UNIX   PDP-11/70   January  1979
    [f]  Research UNIX 8        VAX-11/750           1984
    [g]  AT&T UNIX System III   PDP-11/70   Fall     1982
    [h]  AT&T UNIX System V     PDP-11/70            1983
    [i]  AT&T UNIX System V     3b2/400              1984
    [j]  4.3 BSD                MicroVAX    June     1986
    [k]  2.11 BSD               PDP-11/70   January  1992
    [w]  What's running now?
    [q]  QUIT (and run away in fear!)

    User contributed tutorials are at https://sdf.org/?tutorials/unix50th
    Want persistent images? networking? more ttys? Join https://sdf.org

Your choice? 
```
