from dataclasses import dataclass, field
import argparse
import json
import os


@dataclass
class ProjectInfo:
    uprojectDir: str = os.path.join("..", "..")
    pluginDir: str = os.getcwd()
    uprojectName: str = "ForHealth"

    plugin: dict[str, str] = field(
        default_factory=lambda: {
            "Name": "CMakeUnreal",
            "Enabled": True
        })


class ProjectParser:
    def __init__(self, project_info: ProjectInfo) -> None:
        self.__projDetails: ProjectInfo = project_info
        self.__projFilePath: str = os.path.join(
            self.__projDetails.uprojectDir, self.__projDetails.uprojectName + ".uproject")
        self.__projectFile: list[str] = {}
        self.__read()
        self.__addPlugin()
        self.__export()

    def __read(self) -> None:
        with open(self.__projFilePath, "r+", encoding="utf-8") as p:
            self.__projectFile = json.loads(p.read())

    def __addPlugin(self) -> None:
        self.__projectFile["Plugins"].append(self.__projDetails.plugin)

    def __export(self) -> None:
        json_object = json.dumps(self.__projectFile, indent=4)
        with open(self.__projFilePath, "w") as outfile:
            outfile.write(json_object)


def configure(project_name: str, project_path: str, plugin_path: str):
    p = ProjectInfo(uprojectName=project_name,
                    uprojectDir=project_path, pluginDir=plugin_path)
    ProjectParser(p)


def main():
    parser = argparse.ArgumentParser(
        "Configure CMakeUnreal for use with your Unreal Engine 5 project.")
    parser.add_argument(
        '--PROJECT_NAME', dest='project_name', type=str, required=True)
    parser.add_argument(
        '---PROJECT_PATH', dest='project_path', type=str, default=os.path.join("..", ".."), required=False)
    parser.add_argument(
        '--PLUGIN_PATH', dest='plugin_path', type=str, default=os.getcwd(), required=False)
    args = parser.parse_args()
    configure(args.project_name, args.project_path, args.plugin_path)


if __name__ == "__main__":
    main()
